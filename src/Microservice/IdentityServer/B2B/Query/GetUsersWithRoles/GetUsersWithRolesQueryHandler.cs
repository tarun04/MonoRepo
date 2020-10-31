using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsersWithRoles
{
    public class GetUsersWithRolesQueryHandler : IRequestHandler<GetUsersWithRolesQuery, IReadOnlyList<UserViewModel>>
    {
        private readonly IdentityUser identityUser;
        private readonly IdentityB2BDbContext context;

        public GetUsersWithRolesQueryHandler(IdentityUser identityUser, IdentityB2BDbContext context)
        {
            this.identityUser = identityUser;
            this.context = context;
        }

        public async Task<IReadOnlyList<UserViewModel>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
        {
            return await context.Users
                                .AsNoTracking()
                                .Where(x => x.TenantId == identityUser.TenantId)
                                .Select(x => new UserViewModel
                                {
                                    Id = x.Id.ToString(),
                                    LastName = x.LastName,
                                    FirstName = x.FirstName,
                                    Username = x.UserName,
                                    Email = x.Email,
                                    IsEnabled = x.IsEnabled,
                                    Roles = x.UserRoles.Select(y => new RoleViewModel
                                    {
                                        Id = y.RoleId,
                                        RoleName = y.Role.DisplayName,
                                        Permissions = y.Role.Claims.Select(z => new RoleClaimViewModel
                                        {
                                            RoleClaimType = z.ClaimType,
                                            RoleClaimValue = z.ClaimValue
                                        }),
                                    })
                                })
                                .ToListAsync(cancellationToken);
        }
    }
}
