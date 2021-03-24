using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Domain;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Domain.Enums;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetStudentById
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentByIdViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetStudentByIdQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<GetStudentByIdViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await context.Student
                                       .AsNoTracking()
                                       .Where(x => x.Id == request.Id)
                                       .Include(x => x.StudentCourses)
                                       .ThenInclude(z => z.Course)
                                       .Select(x => new GetStudentByIdViewModel
                                       {
                                           Id = x.Id,
                                           FirstName = x.FirstName,
                                           LastName = x.LastName,
                                           Gender = x.Gender,
                                           Email = x.Email,
                                           PhoneNumber = x.PhoneNumber,
                                           PhoneNumberTypeName = x.PhoneNumberTypeId.HasValue ? Enumeration.FromValue<PhoneType>(x.PhoneNumberTypeId.Value).Name : null,
                                           OtherPhoneNumber = x.OtherPhoneNumber,
                                           Address = x.Address,
                                           BirthDate = x.BirthDate,
                                           AdmissionDate = x.AdmissionDate,
                                           OtherName = x.OtherName,
                                           MiddleName = x.MiddleName,
                                           NameSuffix = x.NameSuffix,
                                           ParentId = x.ParentId,
                                           StudentCourses = x.StudentCourses.Select(y => new StudentCourseViewModel
                                           {
                                               Id = y.CourseId,
                                               Name = y.Course.Name
                                           }).ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            student.Parent = await context.Parent
                                          .AsNoTracking()
                                          .Where(x => x.Id == student.ParentId)
                                          .Select(x => new ParentViewModel
                                          {
                                              Id = x.Id,
                                              FirstName = x.FirstName,
                                              LastName = x.LastName,
                                              RelationTypeId = x.RelationTypeId
                                          })
                                          .FirstOrDefaultAsync(cancellationToken);

            return student;
        }
    }
}
