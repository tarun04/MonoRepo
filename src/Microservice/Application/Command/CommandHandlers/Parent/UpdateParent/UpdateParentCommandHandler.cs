using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Exceptions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.UpdateParent
{
    public class UpdateParentCommandHandler : IRequestHandler<UpdateParentCommand, Unit>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public UpdateParentCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Unit> Handle(UpdateParentCommand request, CancellationToken cancellationToken)
        {
            var parent = await context.Parent
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (parent == null)
                throw new NotFoundException($"Could not find {nameof(Domain.Entities.Parent)} with {nameof(parent.Id)}: {request.Id}.  {nameof(user.TenantId)}: {user.TenantId}");

            parent.UpdateRegistrationInformation(
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.OtherPhoneNumber,
                request.Address);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
