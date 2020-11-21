using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;

namespace MonoRepo.Microservice.Tenant.Infrastructure.EntityConfigurations
{
    public class TenantConfiguration : EntityConfiguration<Domain.Entities.Tenant>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Tenant> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.DisplayName).HasMaxLength(100).IsRequired();
            builder.HasIndex(i => i.Name).IsUnique();

            builder
                .HasMany(x => x.TenantProducts)
                .WithOne(x => x.Tenant)
                .HasForeignKey(fk => fk.TenantId);
        }
    }
}
