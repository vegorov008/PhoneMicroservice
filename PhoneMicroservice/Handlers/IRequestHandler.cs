using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneMicroservice.Handlers
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface IRequestHandler<TRequest, TResponse, TContext> : IRequestHandler<TRequest, TResponse>
    {
        protected TContext Context { get; set; }
    }
}
