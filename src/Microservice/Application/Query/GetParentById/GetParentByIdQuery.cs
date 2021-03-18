using MediatR;

namespace MonoRepo.Microservice.Application.Query.GetParentById
{
    public class GetParentByIdQuery : IRequest<GetParentByIdViewModel>
    {
        public int Id { get; set; }
    }
}
