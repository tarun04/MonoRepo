using MediatR;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.RemoveInstructor
{
    public class RemoveInstructorCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Instructor 
        /// </summary>
        public int Id { get; set; }
    }
}
