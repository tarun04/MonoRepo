using MediatR;
using MonoRepo.Microservice.IdentityServer.B2B.Models;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUser
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public Guid UserId { get; set; }
    }
}
