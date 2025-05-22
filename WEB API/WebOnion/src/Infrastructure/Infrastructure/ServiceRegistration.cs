using Application.Abstractions.Services.Email;
using Application.Abstractions.Services.InMemoryCache;
using Application.Abstractions.Services.Jwt;
using Infrastructure.Services.Email;
using Infrastructure.Services.InMemoryCache;
using Infrastructure.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void RegisterInfrastructureServices( this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IJwtService,JwtService>();

            services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));

        }
    }
}
