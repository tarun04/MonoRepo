using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonoRepo.Framework.Infrastructure.EntityConfigurations;
using MonoRepo.Microservice.Application.Domain.Entities;

namespace MonoRepo.Microservice.Application.Infrastructure.EntityConfigurations
{
    public class InstructorCourseConfiguration : EntityConfiguration<InstructorCourse>
    {
        public override void Configure(EntityTypeBuilder<InstructorCourse> builder)
        {
            builder.HasKey(k => new { k.CourseId, k.InstructorId });

            builder
                .HasOne(o => o.Instructor)
                .WithMany(m => m.InstructorCourses)
                .HasForeignKey(fk => fk.InstructorId)
                .IsRequired();

            builder
                .HasOne(o => o.Course)
                .WithMany(m => m.InstructorCourses)
                .HasForeignKey(fk => fk.CourseId)
                .IsRequired();
        }
    }
}
