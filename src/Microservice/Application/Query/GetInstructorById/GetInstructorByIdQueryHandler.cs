using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Domain;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Domain.Enums;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetInstructorById
{
    public class GetInstructorByIdQueryHandler : IRequestHandler<GetInstructorByIdQuery, GetInstructorByIdViewModel>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetInstructorByIdQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<GetInstructorByIdViewModel> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await context.Instructor
                                       .AsNoTracking()
                                       .Where(x => x.Id == request.Id)
                                       .Include(y => y.InstructorCourses)
                                       .ThenInclude(z => z.Course)
                                       .Select(x => new GetInstructorByIdViewModel
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
                                           InstructorCourses = x.InstructorCourses.Select(y => new InstructorCourseViewModel
                                           {
                                               Id = y.CourseId,
                                               Name = y.Course.Name
                                           }).ToList()
                                       })
                                       .FirstOrDefaultAsync(cancellationToken);

            return student;
        }
    }
}
