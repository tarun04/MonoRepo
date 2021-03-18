namespace MonoRepo.Microservice.Application.Query.GetPagedStudents
{
    public class StudentSummaryViewModel
    {
        /// <summary>
        /// Student unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Student's Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Student's Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Student's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
