using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Tenant.Domain.Entities;

namespace MonoRepo.Microservice.Tenant.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : EntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

            builder
                .HasMany(m => m.TenantProducts)
                .WithOne(p => p.Product)
                .HasForeignKey(fk => fk.ProductId);
        }
    }
}
