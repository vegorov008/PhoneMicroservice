using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Handlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse>
    {
        public abstract Task<TResponse> Execute(TRequest request);
    }

    public abstract class BaseRequestHandler<TRequest, TResponse, TContext> : BaseRequestHandler<TRequest, TResponse>
    {
        protected TContext Context { get; set; }

        public BaseRequestHandler(TContext context) : base()
        {
            Context = context;
        }
    }
}
