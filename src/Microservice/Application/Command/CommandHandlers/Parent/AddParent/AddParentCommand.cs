using MediatR;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.AddParent
{
    public class AddParentCommand : IRequest<int>
    {
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
        public int? PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Parent's Other Phone Number
        /// </summary>
        public string OtherPhoneNumber { get; set; }

        /// <summary>
        /// Parent's Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Type of relation (father/mother/guardian) 
        /// </summary>
        public int? RelationTypeId { get; set; }
    }
}
