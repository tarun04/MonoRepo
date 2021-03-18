using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.RemoveInstructor
{
    public class RemoveInstructorCommandValidator : AbstractValidator<RemoveInstructorCommand>
    {
        public RemoveInstructorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("InstructorId must be provided");
        }
    }
}
