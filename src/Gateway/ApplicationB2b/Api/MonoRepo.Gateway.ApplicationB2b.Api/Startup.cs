using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Extensions.Extensions;
using MonoRepo.Framework.Extensions.Startup;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MonoRepo.Gateway.ApplicationB2b.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("application-gateway",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            services
                .AddLogging()
                .RegisterApplicationInsights(Configuration)
                .RegisterGatewaySecurity(Configuration)
                .AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("application-gateway")
               .UseRouting()
               .UseHttpsRedirection()
               .UseAuthentication()
               .UseProductAuthorization()
               .UseIdentityUserClaimsForwarding()
               .UseExceptionHandlingMiddleware()
               .UseOcelot().Wait();
        }
    }
}
