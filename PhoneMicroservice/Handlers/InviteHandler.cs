using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneMicroservice.Contexts;
using PhoneMicroservice.Helpers;
using PhoneMicroservice.Models;

namespace PhoneMicroservice.Handlers
{
    public class InviteHandler : BaseRequestHandler<InviteRequest, InviteResponse, InviteContext>
    {
        const string PHONE_NUMBERS_INVALID_FORMAT = "BAD_REQUEST PHONE_NUMBERS_INVALID: One or several phone numbers do not match with international format.";
        const string PHONE_NUMBERS_EMPTY = "BAD_REQUEST PHONE_NUMBERS_EMPTY: Phone_numbers is missing.";
        const string PHONE_NUMBERS_LIMIT_PER_REQUEST = "BAD_REQUEST PHONE_NUMBERS_INVALID: Too much phone numbers, should be less or equal to 16 per request.";
        const string PHONE_NUMBERS_LIMIT_PER_DAY = "BAD_REQUEST PHONE_NUMBERS_INVALID: Too much phone numbers, should be less or equal to 128 per day.";
        const string PHONE_NUMBERS_DUPLICATE = "BAD_REQUEST PHONE_NUMBERS_INVALID: Duplicate numbers detected.";
        const string MESSAGE_EMPTY = "BAD_REQUEST MESSAGE_EMPTY: Invite message is missing.";
        const string MESSAGE_INVALID = "BAD_REQUEST MESSAGE_INVALID: Invite message should contain only characters in 7-bit GSM encoding or Cyrillic letters as well.";
        const string MESSAGE_TOO_LONG = "BAD_REQUEST MESSAGE_INVALID: Invite message too long, should be less or equal to 128 characters of 7-bit GSM charset.";
        const string INTERNAL_ERROR = "INTERNAL SMS_SERVICE: {0}";

        const int NUMBERS_PER_REQUEST_LIMIT = 16;
        const int MESSAGE_LENGTH_LIMIT = 160;
        const int INVITES_PER_DAY_LIMIT = 128;

        const int AppId = 4;
        const int UserId = 7;

        enum Responses
        {
            PhoneNumbersInvalidFormat,
            PhoneNumbersEmpty,
            PhoneNumbersLimitPerRequest,
            PhoneNumbersLimitPerDay,
            PhoneNumbersDuplicate,
            MessageEmpty,
            MessageInvalid,
            MessageTooLong,
            Succeess
        }

        Dictionary<Responses, InviteResponse> _responses = new Dictionary<Responses, InviteResponse>()
        {
            { Responses.PhoneNumbersInvalidFormat, new InviteResponse(400, PHONE_NUMBERS_INVALID_FORMAT)},
            { Responses.PhoneNumbersEmpty, new InviteResponse(401, PHONE_NUMBERS_EMPTY)},
            { Responses.PhoneNumbersLimitPerRequest, new InviteResponse(402, PHONE_NUMBERS_LIMIT_PER_REQUEST)},
            { Responses.PhoneNumbersLimitPerDay, new InviteResponse(403, PHONE_NUMBERS_LIMIT_PER_DAY)},
            { Responses.PhoneNumbersDuplicate, new InviteResponse(404, PHONE_NUMBERS_DUPLICATE)},
            { Responses.MessageEmpty, new InviteResponse(405, MESSAGE_EMPTY)},
            { Responses.MessageInvalid, new InviteResponse(406, MESSAGE_INVALID)},
            { Responses.MessageTooLong, new InviteResponse(407, MESSAGE_TOO_LONG)},
            { Responses.Succeess, new InviteResponse(200, string.Empty)}
        };


        readonly IPhoneNumberChecker _phoneNumberChecker;
        readonly IGSMConverter _gsmConverter;

        public InviteHandler(InviteContext context, IPhoneNumberChecker phoneNumberChecker, IGSMConverter gsmConverter) : base(context)
        {
            _phoneNumberChecker = phoneNumberChecker;
            _gsmConverter = gsmConverter;
        }

        public override async Task<InviteResponse> Execute(InviteRequest request)
        {
            InviteResponse response = null;
            try
            {
                if (response == null && request.Phones.IsNullOrEmpty())
                {
                    response = _responses[Responses.PhoneNumbersEmpty];
                }

                if (response == null && request.Message.IsNullOrEmpty())
                {
                    response = _responses[Responses.MessageEmpty];
                }

                if (response == null && request.Phones.Length > NUMBERS_PER_REQUEST_LIMIT)
                {
                    response = _responses[Responses.PhoneNumbersLimitPerRequest];
                }

                if (response == null && !request.Phones.ForAll(x => _phoneNumberChecker.IsPhoneNumber(x)))
                {
                    response = _responses[Responses.PhoneNumbersInvalidFormat];
                }

                if (response == null && request.Phones.ContainsDuplicates())
                {
                    response = _responses[Responses.PhoneNumbersDuplicate];
                }

                if (response == null)
                {
                    var gsmMessage = _gsmConverter.Encode(request.Message);

                    if (gsmMessage.IsNullOrEmpty())
                    {
                        response = _responses[Responses.MessageInvalid];
                    }

                    if (response == null && gsmMessage.Length > MESSAGE_LENGTH_LIMIT)
                    {
                        response = _responses[Responses.MessageTooLong];
                    }

                    if (response == null && await Context.GetInvitationsCount(AppId) > INVITES_PER_DAY_LIMIT)
                    {
                        response = _responses[Responses.PhoneNumbersLimitPerDay];
                    }

                    if (response == null)
                    {
                        await Context.Invite(UserId, request.Phones);
                        response = _responses[Responses.Succeess];
                    }
                }
            }
            catch (Exception ex)
            {
                response = new InviteResponse(500, string.Format(INTERNAL_ERROR, ex.Message));
            }

            return response;
        }
    }
}
