using MediatR;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Course.RemoveCourse
{
    public class RemoveCourseCommand : IRequest<Unit>
    {
        /// <summary>
        /// Id of the Course 
        /// </summary>
        public int Id { get; set; }
    }
}
