using MediatR;
using Microsoft.AspNetCore.Identity;
using MonoRepo.Microservice.IdentityServer.B2B.Domain.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.AddRole
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Guid>
    {
        private readonly Framework.Core.Security.IdentityUser identityUser;
        private readonly RoleManager<Role> roleManager;

        public AddRoleCommandHandler(Framework.Core.Security.IdentityUser identityUser, RoleManager<Role> roleManager)
        {
            this.identityUser = identityUser;
            this.roleManager = roleManager;
        }

        public async Task<Guid> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new Role
            {
                Name = $"{request.RoleName.Replace(" ", "")}_{identityUser.TenantId}",
                DisplayName = request.RoleName,
                RoleDescription = request.RoleDescription,
                TenantId = identityUser.TenantId,
                ProductId = request.ProductId
            };

            var result = await roleManager.CreateAsync(newRole);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(",", result.Errors.Select(x => x.Description)));
            }

            foreach (var permission in request.Permissions)
            {
                await roleManager.AddClaimAsync(newRole, new Claim(permission.RoleClaimType, permission.RoleClaimValue));
            }
            return newRole.Id;
        }
    }
}
