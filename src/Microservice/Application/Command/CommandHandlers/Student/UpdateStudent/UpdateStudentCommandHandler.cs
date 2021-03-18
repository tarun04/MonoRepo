﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Exceptions;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Unit>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public UpdateStudentCommandHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await context.Student
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (student == null)
                throw new NotFoundException($"Could not find {nameof(Domain.Entities.Student)} with {nameof(student.Id)}: {request.Id}.  {nameof(user.TenantId)}: {user.TenantId}");

            student.UpdateRegistrationInformation(
                request.PhoneNumber,
                request.PhoneNumberTypeId,
                request.OtherPhoneNumber,
                request.Address,
                request.OtherName,
                request.MiddleName,
                request.NameSuffix);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
