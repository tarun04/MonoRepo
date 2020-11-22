using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MonoRepo.Microservice.Tenant.Infrastructure;
using System;
using System.IO;

namespace MonoRepo.Microservice.Tenant.Migration
{
    public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Environment.OSVersion.Platform.ToString() == "Unix" ? "appsettings.Unix.json" : "appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<TenantDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new TenantDbContext(builder.Options);
        }
    }
}
