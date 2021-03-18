using MediatR;
using System;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.UpdateInstructor
{
    public class UpdateInstructorCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Instructor 
        /// </summary>
        public int Id { get; set; }

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
    }
}
