using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Tenant.Domain.Entities;

namespace MonoRepo.Microservice.Tenant.Infrastructure.EntityConfigurations
{
    public class TenantProductConfiguration : EntityConfiguration<TenantProduct>
    {
        public override void Configure(EntityTypeBuilder<TenantProduct> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => new { k.TenantId, k.ProductId });
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.TenantId).IsRequired();
            builder.Property(p => p.PurchasedDate).IsRequired();
            builder.Property(p => p.GoLiveDate).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.TenantProductUrl).IsRequired(false);

            builder
                .HasOne(x => x.Tenant)
                .WithMany(x => x.TenantProducts)
                .HasForeignKey(fk => fk.TenantId);
            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.TenantProducts)
                .HasForeignKey(fk => fk.ProductId);
        }
    }
}
