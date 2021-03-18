using MediatR;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Course.AddCourse
{
    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public AddCourseCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<int> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Domain.Entities.Course(
                request.Name,
                request.Description,
                request.Credits,
                request.IsActive);

            context.Course.Add(course);

            await context.SaveChangesAsync(cancellationToken);

            return course.Id;
        }
    }
}
