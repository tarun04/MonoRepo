using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetRoles
{
    public class GetRolesQuery : IRequest<IReadOnlyList<RoleViewModel>>
    {
    }
}
