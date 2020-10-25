using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MonoRepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers AppInsights support. Method also includes Kubernetes Enricher.
    /// </summary>
    public static class AppInsights
    {
        public static IServiceCollection RegisterApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
            services.AddApplicationInsightsKubernetesEnricher();
            return services;
        }
    }
}
