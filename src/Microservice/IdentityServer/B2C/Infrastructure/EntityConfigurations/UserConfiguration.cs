using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.IdentityServer.B2C.Domain.Entities;

namespace MonoRepo.Microservice.IdentityServer.B2C.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(p => p.TenantId).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
        }
    }
}
