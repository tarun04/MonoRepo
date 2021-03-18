namespace MonoRepo.Microservice.Application.Query.GetCourses
{
    public class GetCoursesViewModel
    {
        /// <summary>
        /// Course unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Course name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Course description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Course Credits
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// Flag representing if this course is active
        /// </summary>
        public bool IsActive { get; set; }
    }
}
