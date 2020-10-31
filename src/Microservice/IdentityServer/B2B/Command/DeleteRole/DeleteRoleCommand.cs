using MediatR;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
