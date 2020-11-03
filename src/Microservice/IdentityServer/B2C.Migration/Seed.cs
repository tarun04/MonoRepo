using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;

namespace MonoRepo.Microservice.IdentityServer.B2C.Migration
{
    public class Seed
    {
        public static void SeedIdentityData(ConfigurationDbContext context)
        {
            foreach (var client in Config.GetClients())
            {
                if (!context.Clients.Any(x => x.ClientName == client.ClientName))
                    context.Clients.Add(client.ToEntity());
            }

            foreach (var identityResource in Config.GetIdentityResources())
            {
                if (!context.IdentityResources.Any(x => x.Name == identityResource.Name))
                    context.IdentityResources.Add(identityResource.ToEntity());
            }

            foreach (var apiResource in Config.GetApiResources())
            {
                if (!context.ApiResources.Any(x => x.Name == apiResource.Name))
                    context.ApiResources.Add(apiResource.ToEntity());
            }

            if (context.ChangeTracker.HasChanges())
                context.SaveChanges();
        }
    }
}
