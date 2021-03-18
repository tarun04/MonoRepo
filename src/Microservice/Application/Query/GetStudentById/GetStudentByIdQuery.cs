using MediatR;

namespace MonoRepo.Microservice.Application.Query.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<GetStudentByIdViewModel>
    {
        public int Id { get; set; }
    }
}
