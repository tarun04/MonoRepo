using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Interfaces;
using MonoRepo.Framework.Core.Utility;

namespace MonoRepo.Microservice.Tenant.Extensions.Startup
{
    public static class ScopedServices
    {
        public static IServiceCollection RegisterScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTime, DefaultMonoRepoDateTime>();

            return services;
        }
    }
}
