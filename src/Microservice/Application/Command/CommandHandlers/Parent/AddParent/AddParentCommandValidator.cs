using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.AddParent
{
    public class AddParentCommandValidator : AbstractValidator<AddParentCommand>
    {
        public AddParentCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName must be provided");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName must be provided");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender must be provided");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be provided");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber must be provided");
        }
    }
}
