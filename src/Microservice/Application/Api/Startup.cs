using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonoRepo.Framework.Extensions.Extensions;
using MonoRepo.Framework.Extensions.Startup;
using MonoRepo.Microservice.Application.Command.CommandHandlers.Student.AddStudent;
using MonoRepo.Microservice.Application.Extensions.Startup;
using MonoRepo.Microservice.Application.Infrastructure;
using MonoRepo.Microservice.Application.Query.GetPagedStudents;
using System;
using System.IO;
using System.Reflection;

namespace MonoRepo.Microservice.Application.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment  Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining(typeof(GetPagedStudentsQueryHandler));
                    fv.RegisterValidatorsFromAssemblyContaining(typeof(AddStudentCommandHandler));
                })
                .RegisterJsonOptions();

            services
                .AddLogging()
                .AddMemoryCache()
                .RegisterApplicationInsights(Configuration)
                .RegisterMediatR(Assembly.GetAssembly(typeof(GetPagedStudentsQueryHandler)), Assembly.GetAssembly(typeof(AddStudentCommandHandler)))
                .RegisterApiVersioning()
                .RegisterSwagger("MonoRepo.Microservice.Application.Api", config =>
                {
                    config.Add(Path.Combine(AppContext.BaseDirectory, "MonoRepo.Microservice.Application.Api.xml"));
                    config.Add(Path.Combine(AppContext.BaseDirectory, "MonoRepo.Microservice.Application.Command.xml"));
                    config.Add(Path.Combine(AppContext.BaseDirectory, "MonoRepo.Microservice.Application.Domain.xml"));
                    config.Add(Path.Combine(AppContext.BaseDirectory, "MonoRepo.Microservice.Application.Infrastructure.xml"));
                    config.Add(Path.Combine(AppContext.BaseDirectory, "MonoRepo.Microservice.Application.Query.xml"));
                })
                .RegisterHealthChecks<ApplicationDbContext>(Environment)
                .RegisterDbContext<ApplicationDbContext>(Configuration)
                .RegisterScopedServices(Configuration)
                .RegisterHttpClients(Configuration)
                .RegisterRequestingProduct()
                .RegisterCurrentIdentityUser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseExceptionHandlingMiddleware();
            if (env.IsProduction())
            {
                app.UseHealthChecks("health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("Ready")
                });

                // Here we will call just a quick check to determine if the app is available to process requests.
                // Kubernetes uses this middleware to know when to restart a Container
                app.UseHealthChecks("/health/live", new HealthCheckOptions()
                {
                    // Exclude all checks and return a 200-Ok.
                    Predicate = (_) => false
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                        $"{description.GroupName}/swagger.json",
                        $"MonoRepo.Microservice.Application.Api {description.GroupName}");
                }
            });

            app.UseRequestProductInfo();

            app.UseCurrentUser();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
