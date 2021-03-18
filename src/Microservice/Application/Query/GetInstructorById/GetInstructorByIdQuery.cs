using MediatR;

namespace MonoRepo.Microservice.Application.Query.GetInstructorById
{
    public class GetInstructorByIdQuery : IRequest<GetInstructorByIdViewModel>
    {
        public int Id { get; set; }
    }
}
