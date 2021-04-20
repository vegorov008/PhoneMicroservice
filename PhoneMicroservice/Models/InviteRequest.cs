using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Models
{
    public class InviteRequest
    {
        public string[] Phones { get; set; }
        public string Message { get; set; }
    }
}
