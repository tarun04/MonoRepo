using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Exceptions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.UpdateInstructor
{
    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand, Unit>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public UpdateInstructorCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Unit> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await context.Instructor
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (instructor == null)
                throw new NotFoundException($"Could not find {nameof(Domain.Entities.Instructor)} with {nameof(instructor.Id)}: {request.Id}.  {nameof(user.TenantId)}: {user.TenantId}");

            instructor.UpdateRegistrationInformation(
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.OtherPhoneNumber,
                request.Address);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
