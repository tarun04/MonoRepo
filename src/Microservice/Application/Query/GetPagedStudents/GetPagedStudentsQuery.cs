using MediatR;
using MonoRepo.Framework.Infrastructure.Models;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetPagedStudents
{
    public class GetPagedStudentsQuery : IRequest<PagedResult<StudentSummaryViewModel>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public IEnumerable<FilterRule> FilterRule { get; set; }
    }
}
