using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Course.AddCourse
{
    public class AddCourseCommandValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be provided");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must be provided");
            RuleFor(x => x.Credits).NotEmpty().GreaterThan(0).WithMessage("Credits must be provided");
        }
    }
}
