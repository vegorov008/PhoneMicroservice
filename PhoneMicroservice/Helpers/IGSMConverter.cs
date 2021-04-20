using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Helpers
{
    public interface IGSMConverter
    {
        string Encode(string value);

        string Decode(string value);
    }
}
