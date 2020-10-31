using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IReadOnlyList<UserViewModel>>
    {
        private readonly IdentityUser identityUser;
        private readonly IdentityB2BDbContext context;

        public GetUsersQueryHandler(IdentityUser identityUser, IdentityB2BDbContext context)
        {
            this.identityUser = identityUser;
            this.context = context;
        }

        public async Task<IReadOnlyList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (!request.PermissionType.EndsWith(Globals.ClaimPermissionAppender))
                request.PermissionType = string.Concat(request.PermissionType, Globals.ClaimPermissionAppender);

            return await context.Users
                                .AsNoTracking()
                                .Where(x => x.TenantId == identityUser.TenantId && x.IsEnabled &&
                                            x.UserRoles.Any(y => y.Role.Claims.Any(z => z.ClaimType == request.PermissionType &&
                                                                                        z.ClaimValue == request.ClaimName)))
                                .Select(x => new UserViewModel
                                {
                                    Id = x.Id.ToString(),
                                    Email = x.Email,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName
                                })
                                .ToListAsync(cancellationToken);
        }
    }
}
