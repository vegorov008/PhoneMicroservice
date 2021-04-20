using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Models
{
    public interface IResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
