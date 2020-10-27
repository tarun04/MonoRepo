using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Microservice.IdentityServer.B2B.Helpers;
using MonoRepo.Microservice.IdentityServer.B2B.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2B.ProfileServices;
using MonoRepo.Microservice.IdentityServer.B2B.Services;

namespace MonoRepo.Microservice.IdentityServer.B2B.Extensions.Startup
{
    public static class ScopedServices
    {
        public static IServiceCollection RegisterScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MicroservicesHelper>(options => configuration.GetSection("Microservices").Bind(options));
            services.AddSingleton(configuration.Get<MicroservicesHelper>());
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProfileService, DefaultClaimsProfileService>();
            services.AddTransient<IAccountHelper, AccountHelper>();

            return services;
        }
    }
}
