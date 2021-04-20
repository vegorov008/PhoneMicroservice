using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Contexts
{
    // Implement data acceess with stored procedures, ORM or any other way
    public class InviteContext
    {
        public async Task<int> GetInvitationsCount(int AppId)
        {
            throw new NotImplementedException();
        }

        public async Task Invite(int UserId, string[] phones)
        {
            throw new NotImplementedException();
        }
    }
}
