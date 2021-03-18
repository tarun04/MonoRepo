using MediatR;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetCourses
{
    public class GetCoursesQuery : IRequest<IReadOnlyList<GetCoursesViewModel>>
    {
    }
}
