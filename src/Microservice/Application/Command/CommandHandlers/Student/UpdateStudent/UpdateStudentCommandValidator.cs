using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Student.UpdateStudent
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber must be provided");
        }
    }
}
