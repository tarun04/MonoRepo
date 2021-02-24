using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class ParentConfiguration : EntityConfiguration<Parent>
    {
        public override void Configure(EntityTypeBuilder<Parent> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.IdentityUserId).IsRequired();
            builder.Property(X => X.FirstName).IsRequired().HasMaxLength(500);
            builder.Property(X => X.LastName).IsRequired().HasMaxLength(500);
            builder.Property(X => X.Gender).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(320);
            builder.Property(X => X.PhoneNumber).HasMaxLength(30);
            builder.Property(p => p.PhoneNumberTypeId).IsRequired(false);
            builder.Property(X => X.OtherPhoneNumber).HasMaxLength(30);
            builder.Property(p => p.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(p => p.RelationTypeId).IsRequired(false);

            builder
                .HasMany(m => m.Children)
                .WithOne(o => o.Parent)
                .HasForeignKey(fk => fk.ParentId);
        }
    }
}
