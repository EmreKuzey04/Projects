using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;

namespace Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> :IPipelineBehavior<TRequest,TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;


            LogContext.PushProperty("EventType", 1);

           using(LogContext.PushProperty("UserName", _httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ==true ? 
               _httpContextAccessor.HttpContext.User.Identity.Name : "anonymous" ))
           {
                _logger.LogInformation("REQUEST : {RequestName} | Data : {@Request}", requestName, request);
           }

           
           var stopWatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopWatch.Stop();

                _logger.LogInformation("HANDLED REQUEST : {RequestName} in {ElapsedMilliseconds}ms.| RESPONSE : {@Response}", requestName, stopWatch.ElapsedMilliseconds,response);

                return response;
            }
            catch (Exception)
            {

                throw;
            }

           
        }
    }
}
