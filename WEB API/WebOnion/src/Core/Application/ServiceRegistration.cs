using Application.Behaviors;
using Application.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
 
          services.AddAutoMapper(assembly);

          services.AddMediatR(cnfg => cnfg.RegisterServicesFromAssembly(assembly));

          services.AddValidatorsFromAssembly(assembly); //fluentvalidation register

          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

          services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.AddTransient<ExceptionHandlerMiddleware>(); // exception handler middleware i için register edildi (ımiddleware den )

            


        }
    }
}
