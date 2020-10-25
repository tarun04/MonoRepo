﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MonoRepo.Framework.Extensions.Startup
{
    /// <summary>
    /// Registers DbContext for a given connection.
    /// </summary>
    public static class EntityFramework
    {
        public static IServiceCollection RegisterDbContext<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<T>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                        options.EnableSensitiveDataLogging(true);
                        options.EnableDetailedErrors(true);
                    });
            return services;
        }
    }
}