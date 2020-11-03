using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.ProfileServices;
using System;
using System.Reflection;

namespace MonoRepo.Microservice.IdentityServer.B2B.Extensions.Startup
{
    public static class Identity
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            //
            // Add ASP.net Core Identity
            //
            services.AddIdentity<User, Role>(c => { c.SignIn.RequireConfirmedEmail = true; })
                .AddEntityFrameworkStores<IdentityB2BDbContext>()
                .AddDefaultTokenProviders();

            //
            // Add Identity Server Configuration
            //
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = typeof(B2B.Startup).GetTypeInfo().Assembly.GetName().Name;

            // configure identity server with in-memory stores, keys, clients and scopes
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<User>()
                .AddProfileService<DefaultClaimsProfileService>();

            if (environment.IsProduction())
                throw new NotImplementedException("Identity server is not currently implemented for production!");
            else
                builder.AddDeveloperSigningCredential();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            return services;
        }
    }
}
