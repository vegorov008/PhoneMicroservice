using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneMicroservice.Helpers
{
    public class PhoneNumberChecker : IPhoneNumberChecker
    {
        Regex regex = new Regex("");
        
        public bool IsPhoneNumber(string value)
        {
            return regex.IsMatch(value);
        }
    }
}
