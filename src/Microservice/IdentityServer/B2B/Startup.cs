using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoRepo.Framework.Extensions.Extensions;
using MonoRepo.Framework.Extensions.Startup;
using MonoRepo.Microservice.IdentityServer.B2B.Extensions.Startup;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;

namespace MonoRepo.Microservice.IdentityServer.B2B
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .RegisterDbContext(Configuration)
                .AddIdentity(Configuration, Environment)
                .AddLogging()
                .RegisterApplicationInsights(Configuration)
                .RegisterApiVersioning()
                .RegisterSwagger()
                .RegisterScopedServices(Configuration)
                .RegisterHttpClients(Configuration)
                .RegisterCurrentIdentityUser()
                .RegisterMediatR(Assembly.GetAssembly(typeof(Startup)));

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllersWithViews()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining(typeof(Startup));
                })
                .RegisterJsonOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });

            app.UseForwardedHeaders();
            app.UseExceptionHandlingMiddleware();

            bool.TryParse(Configuration["use-dev-data"], out var useDevData);
            if (useDevData)
            {
                Seed.EnsureSeedData(app);
            }

            app.UseHsts();

            app.UsePathBase(new PathString("/identity-b2b"));

            app.UseHttpsRedirection();

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None,
                HttpOnly = HttpOnlyPolicy.None
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
