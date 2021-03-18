using MediatR;
using System.Collections.Generic;

namespace MonoRepo.Microservice.Application.Query.GetInstructors
{
    public class GetInstructorsQuery : IRequest<IReadOnlyList<GetInstructorsViewModel>>
    {
    }
}
