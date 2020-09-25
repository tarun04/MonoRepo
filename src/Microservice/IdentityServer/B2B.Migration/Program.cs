using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating contexts");
            var configContext = new ConfigurationContextDesignTimeFactory().CreateDbContext(null);
            var persistedGrantContext = new PersistedGrantContextDesignTimeFactory().CreateDbContext(null);
            var identityB2BContext = new IdentityB2BContextFactory().CreateDbContext(null);

            Console.WriteLine("Migrating database");
            configContext.Database.Migrate();
            persistedGrantContext.Database.Migrate();
            identityB2BContext.Database.Migrate();

            Console.WriteLine("Seeding Identity data");
            Seed.SeedIdentityData(configContext);
        }
    }
}
