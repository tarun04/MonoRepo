using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IReadOnlyList<RoleViewModel>>
    {
        private readonly IdentityUser identityUser;
        private readonly IdentityB2BDbContext context;

        public GetRolesQueryHandler(IdentityUser identityUser, IdentityB2BDbContext context)
        {
            this.identityUser = identityUser;
            this.context = context;
        }

        public async Task<IReadOnlyList<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await context.Roles
                                .AsNoTracking()
                                .Where(x => x.TenantId == identityUser.TenantId)
                                .Select(x => new RoleViewModel
                                {
                                    Id = x.Id,
                                    RoleName = x.DisplayName,
                                    RoleDescription = x.RoleDescription,
                                    Permissions = x.Claims.Select(z => new RoleClaimViewModel
                                    {
                                        RoleClaimType = z.ClaimType,
                                        RoleClaimValue = z.ClaimValue
                                    })
                                })
                                .ToListAsync(cancellationToken);
        }
    }
}
