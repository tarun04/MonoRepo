using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class CourseConfiguration : EntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(X => X.Name).IsRequired().HasMaxLength(500);
            builder.Property(X => X.Description).IsRequired().HasMaxLength(500);
            builder.Property(X => X.Credits).IsRequired();
            builder.Property(X => X.IsActive).IsRequired();

            builder
                .HasMany(m => m.InstructorCourses)
                .WithOne(o => o.Course)
                .HasForeignKey(fk => fk.CourseId)
                .IsRequired();

            builder
                .HasMany(m => m.StudentCourses)
                .WithOne(o => o.Course)
                .HasForeignKey(fk => fk.CourseId)
                .IsRequired();

            //builder
            //    .HasMany(m => m.Instructors)
            //    .WithMany(m => m.Courses)
            //    .UsingEntity<InstructorCourses>(
            //        pt => pt.HasOne(p => p.Instructor).WithMany().HasForeignKey("InstructorId"),
            //        pt => pt.HasOne(p => p.Course).WithMany().HasForeignKey("CourseId")
            //    )
            //    .HasKey(pt => new { pt.InstructorId, pt.CourseId });
        }
    }
}
