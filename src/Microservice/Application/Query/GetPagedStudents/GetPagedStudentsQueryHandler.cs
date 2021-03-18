using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonoRepo.Framework.Core.Security;
using MonoRepo.Framework.Infrastructure.Extensions;
using MonoRepo.Framework.Infrastructure.Filtering;
using MonoRepo.Framework.Infrastructure.Models;
using MonoRepo.Microservice.Application.Domain.Entities;
using MonoRepo.Microservice.Application.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonoRepo.Microservice.Application.Query.GetPagedStudents
{
    public class GetPagedStudentsQueryHandler : IRequestHandler<GetPagedStudentsQuery, PagedResult<StudentSummaryViewModel>>
    {
        private readonly ApplicationDbContext context;
        private readonly IdentityUser user;

        public GetPagedStudentsQueryHandler(ApplicationDbContext context, IdentityUser user)
        {
            this.context = context;
            this.user = user;
        }

        public Task<PagedResult<StudentSummaryViewModel>> Handle(GetPagedStudentsQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.New<Student>();

            if (request.FilterRule != null && request.FilterRule.Any())
            {
                var filters = new ExpressionFactory().BuildExpression<Student>(request.FilterRule);
                predicate = predicate.And(filters);
            }

            return context.Student
                          .AsNoTracking()
                          .Where(predicate)
                          .Select(x => new StudentSummaryViewModel
                          {
                              Id = x.Id,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              Gender = x.Gender,
                              Email = x.Email,
                              PhoneNumber = x.PhoneNumber
                          })
                          .GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
