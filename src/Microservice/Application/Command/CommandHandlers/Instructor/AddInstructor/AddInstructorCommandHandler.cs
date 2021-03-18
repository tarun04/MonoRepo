using MediatR;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.AddInstructor
{
    public class AddInstructorCommandHandler : IRequestHandler<AddInstructorCommand, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public AddInstructorCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<int> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = new Domain.Entities.Instructor(
                new Guid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.Address,
                request.JoiningDate);

            context.Instructor.Add(instructor);

            await context.SaveChangesAsync(cancellationToken);

            return instructor.Id;
        }
    }
}
