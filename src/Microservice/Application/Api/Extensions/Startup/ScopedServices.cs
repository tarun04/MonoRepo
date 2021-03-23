using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Interfaces;
using MonoRepo.Framework.Core.Utility;
using MonoRepo.Framework.Identity.Interfaces;
using MonoRepo.Framework.Identity.Services;
using MonoRepo.Framework.Infrastructure.Utility;
using MonoRepo.Framework.Utilities.Validation;
using MonoRepo.Microservice.Application.Infrastructure;

namespace MonoRepo.Microservice.Application.Extensions.Startup
{
    public static class ScopedServices
    {
        public static IServiceCollection RegisterScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTime, DefaultMonoRepoDateTime>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IConfigHelper, ConfigHelper<ApplicationDbContext>>();
            services.AddScoped<IIdentityB2bService, IdentityB2bService>();
            services.Configure<MicroservicesHelper>(options => configuration.GetSection("Microservices").Bind(options));
            services.AddSingleton(configuration.Get<MicroservicesHelper>());

            return services;
        }
    }
}
