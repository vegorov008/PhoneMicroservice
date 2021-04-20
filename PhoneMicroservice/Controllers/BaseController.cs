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
    public class BaseController : ControllerBase
    {
        protected virtual async Task<IActionResult> Post<TRequest, TResponse>(TRequest request)
            where TResponse : IResponse
        {
            ActionResult result = null;

            var response = await Ioc.Resolve<IRequestHandler<TRequest, TResponse>>().Execute(request);
            if (response != null)
            {
                result = Content(response.Message);
                Response.StatusCode = response.Code;
            }
            
            if (result == null)
            {
                result = BadRequest();
            }

            return result;
        }
    }
}
