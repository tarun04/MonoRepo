using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure
{
    public class IdentityB2CContextFactory : DesignTimeDbContextFactoryBase<IdentityB2CDbContext>
    {
        public IdentityB2CContextFactory()
            : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }

        protected override IdentityB2CDbContext CreateNewInstance(DbContextOptions<IdentityB2CDbContext> options)
        {
            return new IdentityB2CDbContext(options);
        }
    }

    public class ConfigurationContextDesignTimeFactory : DesignTimeDbContextFactoryBase<ConfigurationDbContext>
    {
        public ConfigurationContextDesignTimeFactory()
            : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }

        protected override ConfigurationDbContext CreateNewInstance(DbContextOptions<ConfigurationDbContext> options)
        {
            return new ConfigurationDbContext(options, new ConfigurationStoreOptions());
        }
    }

    public class PersistedGrantContextDesignTimeFactory : DesignTimeDbContextFactoryBase<PersistedGrantDbContext>
    {
        public PersistedGrantContextDesignTimeFactory()
            : base("DefaultConnection", typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
        { }

        protected override PersistedGrantDbContext CreateNewInstance(DbContextOptions<PersistedGrantDbContext> options)
        {
            return new PersistedGrantDbContext(options, new OperationalStoreOptions());
        }
    }

    public abstract class DesignTimeDbContextFactoryBase<TContext>
        : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        protected string ConnectionStringName { get; }
        protected string MigrationsAssemblyName { get; }

        public DesignTimeDbContextFactoryBase(string connectionStringName, string migrationsAssemblyName)
        {
            ConnectionStringName = connectionStringName;
            MigrationsAssemblyName = migrationsAssemblyName;
        }

        public TContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                ConnectionStringName, MigrationsAssemblyName);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        public TContext CreateWithConnectionStringName(string connectionStringName, string migrationsAssemblyName)
        {
            return Create(
                AppContext.BaseDirectory,
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                connectionStringName, migrationsAssemblyName);
        }

        private TContext Create(string basePath, string environmentName, string connectionStringName, string migrationsAssemblyName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString(connectionStringName);

            if (String.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException("Could not find a connection string named 'default'.");
            }
            else
            {
                return CreateWithConnectionString(connstr, migrationsAssemblyName);
            }
        }

        private TContext CreateWithConnectionString(string connectionString, string migrationsAssemblyName)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                sqlServerOptions => sqlServerOptions.MigrationsAssembly(migrationsAssemblyName));

            DbContextOptions<TContext> options = optionsBuilder.Options;

            return CreateNewInstance(options);
        }
    }
}
