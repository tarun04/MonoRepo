using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetInstructorById
{
    public class GetInstructorByIdViewModel
    {
        /// <summary>
        /// Instructor unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Instructor's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Instructor's Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Instructor's Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Instructor's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Instructor's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Instructor's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Instructor's Address
        /// </summary>
        public string Address { get; set; }

        public ICollection<InstructorCourseViewModel> InstructorCourses { get; set; }
    }

    public class InstructorCourseViewModel
    {
        /// <summary>
        /// Course unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Course name
        /// </summary>
        public string Name { get; set; }
    }
}
