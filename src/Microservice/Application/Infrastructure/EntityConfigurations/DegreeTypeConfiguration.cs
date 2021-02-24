using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class DegreeTypeConfiguration : EntityConfiguration<DegreeType>
    {
        public override void Configure(EntityTypeBuilder<DegreeType> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(X => X.Name).IsRequired().HasMaxLength(500);
            builder.Property(X => X.Description).IsRequired().HasMaxLength(500);
            builder.Property(X => X.Credits).IsRequired();
            builder.Property(X => X.IsActive).IsRequired();

            builder
                .HasMany(m => m.Degrees)
                .WithOne(o => o.DegreeType)
                .HasForeignKey(fk => fk.DegreeTypeId)
                .IsRequired();
        }
    }
}
