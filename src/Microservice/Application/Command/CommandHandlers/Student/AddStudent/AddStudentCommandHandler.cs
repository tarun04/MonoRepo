using MediatR;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.AddStudent
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, int>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public AddStudentCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<int> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Domain.Entities.Student(
                new Guid(),
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.Address,
                request.BirthDate,
                request.AdmissionDate);

            context.Student.Add(student);

            await context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}
