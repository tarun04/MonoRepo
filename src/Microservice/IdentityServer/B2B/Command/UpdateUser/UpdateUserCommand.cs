using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public IList<RoleViewModel> Roles { get; set; }
    }
}
