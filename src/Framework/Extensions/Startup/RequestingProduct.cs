using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers requesting product info.
    /// </summary>
    public static class RequestingProduct
    {
        public static IServiceCollection RegisterRequestingProduct(this IServiceCollection services)
        {
            services.AddScoped<RequestingProductInfo>();
            return services;
        }
    }
}
