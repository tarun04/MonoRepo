using MediatR;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly UserManager<User> userManager;

        public DeleteUserCommandHandler(Framework.Core.Security.IdentityUser identityUser, UserManager<User> userManager)
        {
            this.identityUser = identityUser;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await userManager.FindByIdAsync(request.Id.ToString());

            if (!(identityUser.TenantId == userToDelete.TenantId))
            {
                throw new InvalidOperationException($"Delete operation not allowed.");
            }

            await userManager.DeleteAsync(userToDelete);
            return Unit.Value;
        }
    }
}
