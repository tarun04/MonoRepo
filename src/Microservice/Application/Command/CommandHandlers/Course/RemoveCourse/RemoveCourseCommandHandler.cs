using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Exceptions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Course.RemoveCourse
{
    public class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand, Unit>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public RemoveCourseCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Unit> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Course
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (course == null)
                throw new NotFoundException($"Could not find {nameof(Domain.Entities.Course)} with {nameof(course.Id)}: {request.Id}.  {nameof(user.TenantId)}: {user.TenantId}");

            context.Course.Remove(course);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
