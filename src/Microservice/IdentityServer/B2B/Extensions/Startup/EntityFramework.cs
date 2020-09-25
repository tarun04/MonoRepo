using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;

namespace MonoRepo.Microservice.IdentityServer.B2B.Extensions.Startup
{
    public static class EntityFramework
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityB2BDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
