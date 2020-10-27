using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Microservice.IdentityServer.B2C.Helpers;
using MonoRepo.Microservice.IdentityServer.B2C.Interfaces;
using MonoRepo.Microservice.IdentityServer.B2C.ProfileServices;
using MonoRepo.Microservice.IdentityServer.B2C.Services;

namespace MonoRepo.Microservice.IdentityServer.B2C.Extensions.Startup
{
    public static class ScopedServices
    {
        public static IServiceCollection RegisterScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MicroservicesHelper>(options => configuration.GetSection("Microservices").Bind(options));
            services.AddSingleton(configuration.Get<MicroservicesHelper>());
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IProfileService, DefaultClaimsProfileService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAccountHelper, AccountHelper>();

            return services;
        }
    }
}
