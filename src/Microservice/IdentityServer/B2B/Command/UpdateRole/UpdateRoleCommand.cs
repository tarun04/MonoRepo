using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public IList<RoleClaimViewModel> Permissions { get; set; }
    }
}
