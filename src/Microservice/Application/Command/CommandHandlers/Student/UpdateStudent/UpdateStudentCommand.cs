using MediatR;
using System;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Student 
        /// </summary>
        public int Id { get; set; }

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
        /// Other Name
        /// </summary>
        public string OtherName { get; set; }

        /// <summary>
        /// MiddleName
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// E.g. Mr.
        /// </summary>
        public string NameSuffix { get; set; }
    }
}
