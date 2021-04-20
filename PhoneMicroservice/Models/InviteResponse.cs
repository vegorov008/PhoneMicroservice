using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Models
{
    public class InviteResponse : IResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public InviteResponse()
        {

        }

        public InviteResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

    }
}
