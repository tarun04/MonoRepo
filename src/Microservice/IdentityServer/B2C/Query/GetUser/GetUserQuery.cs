using MediatR;
using MonoRepo.Microservice.IdentityServer.B2C.Models;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2C.Query.GetUser
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public Guid TenantId { get; set; }
        public string Email { get; set; }
    }
}
