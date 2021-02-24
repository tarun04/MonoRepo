using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class StudentCourse : Entity
    {
        /// <summary>
        /// Id of the Student
        /// </summary>
        public int StudentId { get; private set; }

        /// <summary>
        /// Student Navigation Property
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Id of the Course
        /// </summary>
        public int CourseId { get; private set; }

        /// <summary>
        /// Course Navigation Property
        /// </summary>
        public Course Course { get; private set; }
    }
}
