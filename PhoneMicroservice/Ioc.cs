using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice
{
    /// <summary>
    /// Wrap an existing implementation or code your own
    /// </summary>
    public static class Ioc
    {
        public static T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public static void RegisterSingleton<T>(T singleton)
        {
            throw new NotImplementedException();
        }

        public static void Register<TClass>()
        {
            // should provide instnces to constructor
            throw new NotImplementedException();
        }

        public static void Register<TInterface, TImplementation>()
        {
            // should provide instnces to constructor
            throw new NotImplementedException();
        }
    }
}
