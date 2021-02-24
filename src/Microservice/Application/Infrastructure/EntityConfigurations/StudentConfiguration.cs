using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class StudentConfiguration : EntityConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
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
            builder.Property(p => p.BirthDate).IsRequired(false);
            builder.Property(p => p.AdmissionDate).IsRequired(false);
            builder.Property(X => X.OtherName).IsRequired(false).HasMaxLength(500);
            builder.Property(X => X.MiddleName).IsRequired(false).HasMaxLength(500);
            builder.Property(X => X.NameSuffix).IsRequired(false).HasMaxLength(10);

            builder
                .HasOne(o => o.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(fk => fk.ParentId)
                .IsRequired();

            builder
                .HasMany(m => m.StudentCourses)
                .WithOne(o => o.Student)
                .HasForeignKey(fk => fk.StudentId)
                .IsRequired();

            builder
                .HasMany(m => m.Degrees)
                .WithOne(o => o.Student)
                .HasForeignKey(fk => fk.StudentId)
                .IsRequired();
        }
    }
}
