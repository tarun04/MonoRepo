using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IReadOnlyList<GetCoursesViewModel>>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetCoursesQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<IReadOnlyList<GetCoursesViewModel>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return await context.Course
                                .AsNoTracking()
                                .Where(x => x.IsActive == true)
                                .Select(x => new GetCoursesViewModel
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Description = x.Description,
                                    Credits = x.Credits
                                })
                                .ToListAsync(cancellationToken);
        }
    }
}
