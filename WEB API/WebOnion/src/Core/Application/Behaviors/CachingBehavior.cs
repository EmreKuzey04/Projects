using Application.Abstractions.Services.InMemoryCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachableQuery
    {
        private readonly ICacheService _cacheService;

        public CachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           var cachedResponse = _cacheService.Get<TResponse>(request.CacheKey);

            if (cachedResponse != null)
                return cachedResponse;

           var response = await next();

            _cacheService.Set(request.CacheKey, response, TimeSpan.FromMinutes (request.CacheTime));
            return response;
        }
    }
}
