using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Aggregates.DegreeAggregate;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class DegreeConfiguration : EntityConfiguration<Degree>
    {
        public override void Configure(EntityTypeBuilder<Degree> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(X => X.StartDate).IsRequired();
            builder.Property(X => X.CreditsCompleted).IsRequired();
            builder.Property(X => X.GraduationDate).IsRequired();
            builder.Property(X => X.IsCompleted).IsRequired();
            builder.Property(X => X.AquiredGPA).IsRequired();

            builder
                .HasOne(o => o.Student)
                .WithMany(m => m.Degrees)
                .HasForeignKey(fk => fk.StudentId)
                .IsRequired();

            builder
                .HasOne(o => o.DegreeType)
                .WithMany(m => m.Degrees)
                .HasForeignKey(fk => fk.DegreeTypeId)
                .IsRequired();
        }
    }
}
