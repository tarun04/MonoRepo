using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security.ProductPermissions.Application;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly IdentityB2BDbContext context;

        public UpdateRoleCommandHandler(Framework.Core.Security.IdentityUser identityUser, IdentityB2BDbContext context)
        {
            this.identityUser = identityUser;
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleToUpdate = await context.Roles.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == request.Id);

            if (!(identityUser.TenantId == roleToUpdate.TenantId))
            {
                throw new InvalidOperationException($"Update operation not allowed.");
            }

            roleToUpdate.RoleDescription = request.RoleDescription;
            roleToUpdate.DisplayName = request.RoleName;
            roleToUpdate.Name = $"{request.RoleName.Replace(" ", "")}_{identityUser.TenantId}";
            roleToUpdate.NormalizedName = $"{request.RoleName.Replace(" ", "")}_{identityUser.TenantId}".ToUpper();
            
            roleToUpdate.Claims.Clear();

            roleToUpdate.Claims = request.Permissions.Select(x => new IdentityRoleClaim<Guid> { ClaimType = ApplicationPermissions.ProductPermissionType, ClaimValue = x.RoleClaimValue.Replace(" ", "") }).ToList();

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
