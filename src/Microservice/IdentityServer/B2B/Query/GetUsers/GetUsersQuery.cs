using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsers
{
    public class GetUsersQuery : IRequest<IReadOnlyList<UserViewModel>>
    {
        public string PermissionType { get; set; }
        public string ClaimName { get; set; }
    }
}
