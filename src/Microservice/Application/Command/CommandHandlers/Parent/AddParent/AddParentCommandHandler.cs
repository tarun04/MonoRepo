using MediatR;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.AddParent
{
    public class AddParentCommandHandler : IRequestHandler<AddParentCommand, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public AddParentCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<int> Handle(AddParentCommand request, CancellationToken cancellationToken)
        {
            var parent = new Domain.Entities.Parent(
                new Guid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.Address,
                request.RelationTypeId);

            context.Parent.Add(parent);

            await context.SaveChangesAsync(cancellationToken);

            return parent.Id;
        }
    }
}
