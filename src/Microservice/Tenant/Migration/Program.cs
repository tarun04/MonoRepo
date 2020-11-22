using CommandLine;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace MonoRepo.Microservice.Tenant.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                          .WithParsed<Options>(o =>
                          {
                              if (o.UseDevData)
                              {
                                  CreateAndMigrateDatabase(true);
                              }
                              else
                              {
                                  CreateAndMigrateDatabase();
                              }
                          });
        }

        private static void CreateAndMigrateDatabase(bool useDevData = false)
        {
            Console.WriteLine("Creating context");
            var context = new TenantDbContextFactory().CreateDbContext(null);

            Console.WriteLine("Migrating database");
            context.Database.Migrate();

            if (useDevData)
            {
                Console.WriteLine("Seeding Dev Data");
                context.Database.ExecuteSqlRaw(File.ReadAllText("./Sql/Dev/seed.sql"));
            }

            Console.WriteLine("Complete.");
        }
    }

    class Options
    {
        [Option("use-dev-data", Hidden = false, Required = false)]
        public bool UseDevData { get; set; }
    }
}
