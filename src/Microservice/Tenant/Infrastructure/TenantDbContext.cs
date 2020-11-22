using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Interfaces;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Framework.Infrastructure;
using MonoRepo.Microservice.Tenant.Domain.Entities;
using MonoRepo.Microservice.Tenant.Infrastructure.EntityConfigurations;

namespace MonoRepo.Microservice.Tenant.Infrastructure
{
    public class TenantDbContext : BaseDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Domain.Entities.Tenant> Tenants { get; set; }
        public DbSet<TenantProduct> TenantProducts { get; set; }

        public TenantDbContext(DbContextOptions options, IMediator mediator, IdentityUser user, IDateTime dateTime)
            : base(options, mediator, user, dateTime) { }

        public TenantDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

    }
}
