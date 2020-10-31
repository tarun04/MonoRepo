using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetPermissions
{
    public class GetPermissionsQuery : IRequest<IEnumerable<RoleClaimViewModel>>
    {
    }
}
