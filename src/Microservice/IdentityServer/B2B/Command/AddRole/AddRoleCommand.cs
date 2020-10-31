using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.AddRole
{
    public class AddRoleCommand : IRequest<Guid>
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public Guid ProductId { get; set; }
        public IList<RoleClaimViewModel> Permissions { get; set; }
    }
}
