using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Instructor.UpdateInstructor
{
    public class UpdateInstructorCommandValidator : AbstractValidator<UpdateInstructorCommand>
    {
        public UpdateInstructorCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber must be provided");
        }
    }
}
