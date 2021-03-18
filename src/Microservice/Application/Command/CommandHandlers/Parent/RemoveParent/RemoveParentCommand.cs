using MediatR;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.RemoveParent
{
    public class RemoveParentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Parent 
        /// </summary>
        public int Id { get; set; }
    }
}
