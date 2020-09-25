using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;

namespace MonoRepo.Microservice.IdentityServer.B2B.Infrastructure.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasOne(m => m.User)
                .WithMany(o => o.UserRoles)
                .HasForeignKey(fk => fk.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(m => m.Role)
                .WithMany(o => o.UserRoles)
                .HasForeignKey(fk => fk.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
