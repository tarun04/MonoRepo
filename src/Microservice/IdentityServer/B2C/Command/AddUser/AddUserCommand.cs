using MediatR;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Command.AddUser
{
    public class AddUserCommand : IRequest<UserViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid TenantId { get; set; }
        public bool SendConfirmationEmail { get; set; } = true;
    }
}
