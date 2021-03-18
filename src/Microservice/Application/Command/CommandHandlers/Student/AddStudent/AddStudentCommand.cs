using MediatR;
using System;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.AddStudent
{
    public class AddStudentCommand : IRequest<int>
    {
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

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Student's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Student's Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Student's Birth Date
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Student's admission date
        /// </summary>
        public DateTime? AdmissionDate { get; set; }

        /// <summary>
        /// Id of the Parent
        /// </summary>
        public int ParentId { get; set; }
    }
}
