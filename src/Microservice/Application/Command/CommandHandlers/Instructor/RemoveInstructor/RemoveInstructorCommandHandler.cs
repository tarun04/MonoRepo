using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Exceptions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.RemoveInstructor
{
    public class RemoveInstructorCommandHandler : IRequestHandler<RemoveInstructorCommand, Unit>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public RemoveInstructorCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Unit> Handle(RemoveInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await context.Instructor
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (instructor == null)
                throw new NotFoundException($"Could not find {nameof(Domain.Entities.Instructor)} with {nameof(instructor.Id)}: {request.Id}.  {nameof(user.TenantId)}: {user.TenantId}");

            context.Instructor.Remove(instructor);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
