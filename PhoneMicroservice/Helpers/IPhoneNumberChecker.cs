using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Helpers
{
    public interface IPhoneNumberChecker
    {
        bool IsPhoneNumber(string value);
    }
}
