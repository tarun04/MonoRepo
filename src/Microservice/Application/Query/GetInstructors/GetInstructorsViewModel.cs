using System;

namespace MonoRepo.Microservice.Application.Query.GetInstructors
{
    public class GetInstructorsViewModel
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

        /// <summary>
        /// Instructor's Joining Date
        /// </summary>
        public DateTime? JoiningDate { get; set; }
    }
}
