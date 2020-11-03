using CommandLine;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2C.Infrastructure;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.UseDevData)
                           CreateAndMigrateDatabase(true);
                       else
                           CreateAndMigrateDatabase();
                   });
        }

        public static void CreateAndMigrateDatabase(bool useDevData = false)
        {
            Console.WriteLine("Creating Contexts");
            var configContext = new ConfigurationContextDesignTimeFactory().CreateDbContext(null);
            var persistedGrantContext = new PersistedGrantContextDesignTimeFactory().CreateDbContext(null);
            var identityB2CContext = new IdentityB2CContextFactory().CreateDbContext(null);

            Console.WriteLine("Migrating database");
            configContext.Database.Migrate();
            persistedGrantContext.Database.Migrate();
            identityB2CContext.Database.Migrate();

            Console.WriteLine("Seeding Identity Data");
            Seed.SeedIdentityData(configContext);

            Console.WriteLine("Complete.");
        }
    }

    class Options
    {
        [Option("use-dev-data", Hidden = false, Required = false)]
        public bool UseDevData { get; set; }
    }
}
