using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;

namespace MonoRepo.Microservice.IdentityServer.B2B.Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.TenantId).IsRequired();
            builder.Property(x => x.DisplayName).IsRequired();
            builder.Property(x => x.IsProductDefault).HasDefaultValue(false);

            builder
                .HasMany(m => m.Claims)
                .WithOne()
                .HasForeignKey(fk => fk.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
