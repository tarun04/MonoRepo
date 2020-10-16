using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;

namespace MonoRepo.Microservice.IdentityServer.B2C.Extensions.Startup
{
    public static class EntityFramework
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityB2CDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
