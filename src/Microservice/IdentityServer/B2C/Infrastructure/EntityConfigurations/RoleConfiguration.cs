using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.IsProductDefault).HasDefaultValue(false);
        }
    }
}
