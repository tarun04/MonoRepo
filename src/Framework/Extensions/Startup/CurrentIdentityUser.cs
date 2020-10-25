using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Security;

namespace MonoRepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers current Identity user.
    /// </summary>
    public static class CurrentIdentityUser
    {
        public static IServiceCollection RegisterCurrentIdentityUser(this IServiceCollection services)
        {
            services.AddScoped<IdentityUser>();
            return services;
        }
    }
}
