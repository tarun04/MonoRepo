using MediatR;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly RoleManager<Role> roleManager;

        public DeleteRoleCommandHandler(Framework.Core.Security.IdentityUser identityUser, RoleManager<Role> roleManager)
        {
            this.identityUser = identityUser;
            this.roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleToDelete = await roleManager.FindByIdAsync(request.Id.ToString());

            if (!(identityUser.TenantId == roleToDelete.TenantId))
            {
                throw new InvalidOperationException($"Delete operation not allowed.");
            }

            await roleManager.DeleteAsync(roleToDelete);
            return Unit.Value;
        }
    }
}
