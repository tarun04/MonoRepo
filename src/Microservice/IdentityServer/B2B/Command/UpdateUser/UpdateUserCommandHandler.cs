using MediatR;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly UserManager<User> userManager;

        public UpdateUserCommandHandler(Framework.Core.Security.IdentityUser identityUser, UserManager<User> userManager)
        {
            this.identityUser = identityUser;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await userManager.FindByIdAsync(request.Id.ToString());

            if (!(identityUser.TenantId == userToUpdate.TenantId))
            {
                throw new InvalidOperationException("Update operation not allowed.");
            }

            userToUpdate.FirstName = request.FirstName;
            userToUpdate.LastName = request.LastName;
            userToUpdate.Email = request.Email;
            userToUpdate.IsEnabled = request.IsEnabled;

            await userManager.UpdateAsync(userToUpdate);

            var assignedRoles = await userManager.GetRolesAsync(userToUpdate);
            await userManager.RemoveFromRolesAsync(userToUpdate, assignedRoles);

            await userManager.AddToRolesAsync(userToUpdate, request.Roles.Select(x => $"{x.RoleName.Replace(" ", "")}_{identityUser.TenantId}"));

            return Unit.Value;
        }
    }
}
