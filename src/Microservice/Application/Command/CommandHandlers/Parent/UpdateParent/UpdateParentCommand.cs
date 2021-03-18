using MediatR;
using System;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.UpdateParent
{
    public class UpdateParentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Parent 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Parent's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Type of phone number (mobile/landline) 
        /// </summary>
        public int? PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Parent's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Parent's Address
        /// </summary>
        public string Address { get; set; }
    }
}
