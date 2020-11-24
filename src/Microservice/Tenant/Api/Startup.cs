using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonoRepo.Framework.Extensions.Startup;
using MonoRepo.Microservice.Tenant.Extensions.Startup;
using MonoRepo.Microservice.Tenant.Infrastructure;
using MonoRepo.Microservice.Tenant.Query.GetTenantById;
using System.Reflection;

namespace MonoRepo.Microservice.Tenant.Api
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
                .AddControllers()
                .RegisterJsonOptions();

            services
                .AddMemoryCache()
                .RegisterApplicationInsights(Configuration)
                .RegisterMediatR(Assembly.GetAssembly(typeof(GetTenantByIdQueryHandler)))
                .RegisterApiVersioning()
                .RegisterSwagger("MonoRepo.Microservice.Tenant.Api")
                .RegisterScopedServices(Configuration)
                .RegisterDbContext<TenantDbContext>(Configuration)
                .RegisterHealthChecks<TenantDbContext>(Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                        $"{description.GroupName}/swagger.json",
                        $"MonoRepo.Microservice.Tenant.Api {description.GroupName}");
                }

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
