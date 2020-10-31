using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Microservice.IdentityServer.B2B.Infrastructure;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IdentityB2BDbContext context;

        public GetUserQueryHandler(IdentityB2BDbContext context)
        {
            this.context = context;
        }

        public Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return context.Users
                          .AsNoTracking()
                          .Select(x => new UserViewModel
                          {
                              Id = x.Id.ToString(),
                              LastName = x.LastName,
                              FirstName = x.FirstName,
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
                          .FirstOrDefaultAsync(x => x.Id == request.UserId.ToString(), cancellationToken);
        }
    }
}
