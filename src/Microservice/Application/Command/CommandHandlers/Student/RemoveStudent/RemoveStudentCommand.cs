using MediatR;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.RemoveStudent
{
    public class RemoveStudentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Student 
        /// </summary>
        public int Id { get; set; }
    }
}
