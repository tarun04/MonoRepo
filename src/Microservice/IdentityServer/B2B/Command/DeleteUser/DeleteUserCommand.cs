using MediatR;
using System;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
