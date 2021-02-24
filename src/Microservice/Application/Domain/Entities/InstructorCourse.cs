using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Microservice.Application.Domain.Entities
{
    public class InstructorCourse : Entity
    {
        /// <summary>
        /// Id of the Instructor
        /// </summary>
        public int InstructorId { get; private set; }

        /// <summary>
        /// Instructor Navigation Property
        /// </summary>
        public Instructor Instructor { get; private set; }

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
