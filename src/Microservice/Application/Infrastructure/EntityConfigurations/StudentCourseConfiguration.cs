using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class StudentCourseConfiguration : EntityConfiguration<StudentCourse>
    {
        public override void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(k => new { k.CourseId, k.Student });

            builder
                .HasOne(o => o.Student)
                .WithMany(m => m.StudentCourses)
                .HasForeignKey(fk => fk.StudentId)
                .IsRequired();

            builder
                .HasOne(o => o.Course)
                .WithMany(m => m.StudentCourses)
                .HasForeignKey(fk => fk.CourseId)
                .IsRequired();
        }
    }
}
