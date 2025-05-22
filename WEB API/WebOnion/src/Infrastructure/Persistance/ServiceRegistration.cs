using Domain.Entities.Identity;
using Domain.Interfaces.Repositories.Products;
using Domain.Interfaces.Repositories.Shippers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories.Products;
using Persistance.Repositories.Shippers;

namespace Persistance
{
    public static class ServiceRegistration
    {
        public static void RegisterPersistanceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TradewndContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServerConnStr"))
            .ConfigureWarnings(w =>
            w.Ignore(RelationalEventId.PendingModelChangesWarning)));

            services.AddScoped<IProductQueryRepository,ProductQueryRepository>();
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            //services.AddScoped<ICategoryQueryRepository,CategoryQueryRepository> ();
            //services.AddScoped<ISupplierQueryRepository,SupplierQueryRepository> ();
            
            services.AddScoped<IShipperQueryRepository, ShipperQueryRepository>();
            services.AddScoped<IShipperCommandRepository, ShipperCommandRepository>();

           

            services.AddIdentity<AppUser, AppRole>(opt =>
            {


            })
                .AddEntityFrameworkStores<TradewndContext>()
                .AddDefaultTokenProviders();


        }
    }
}
