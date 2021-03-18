using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Course.RemoveCourse
{
    public class RemoveCourseCommandValidator : AbstractValidator<RemoveCourseCommand>
    {
        public RemoveCourseCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("CourseId must be provided");
        }
    }
}
