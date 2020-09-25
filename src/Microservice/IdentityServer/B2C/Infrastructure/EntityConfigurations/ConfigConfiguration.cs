using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure.EntityConfigurations
{
    public class ConfigConfiguration : IEntityTypeConfiguration<Config>
    {
        public void Configure(EntityTypeBuilder<Config> builder)
        {
            builder.Property(p => p.ConfigId).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.TenantId).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Order).IsRequired(false);
        }
    }
}
