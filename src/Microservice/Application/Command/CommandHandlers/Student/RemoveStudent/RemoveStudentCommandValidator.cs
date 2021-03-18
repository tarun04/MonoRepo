using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.RemoveStudent
{
    public class RemoveStudentCommandValidator : AbstractValidator<RemoveStudentCommand>
    {
        public RemoveStudentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("StudentId must be provided");
        }
    }
}
