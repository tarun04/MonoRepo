using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetParentById
{
    public class GetParentByIdViewModel
    {
        /// <summary>
        /// Parent unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Parent's First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Parent's Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Parent's Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Parent's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Parent's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public string PhoneNumberTypeName { get; set; }

        /// <summary>
        /// Parent's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Parent's Address
        /// </summary>
        public string Address { get; set; }

        public ICollection<StudentViewModel> Children { get; set; }
    }

    public class StudentViewModel
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
    }
}
