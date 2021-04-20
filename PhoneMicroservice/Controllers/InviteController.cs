using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneMicroservice.Handlers;
using PhoneMicroservice.Models;

namespace PhoneMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController : BaseController
    {
        [Route("some_route")]
        public async Task<IActionResult> SendInvites(string[] phones, string message)
        {
            InviteRequest request = new InviteRequest()
            {
                Phones = phones,
                Message = message
            };

            return await Post<InviteRequest, InviteResponse>(request);
        }
    }
}
