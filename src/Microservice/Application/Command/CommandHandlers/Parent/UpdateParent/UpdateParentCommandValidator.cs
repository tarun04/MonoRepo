using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.UpdateParent
{
    public class UpdateParentCommandValidator : AbstractValidator<UpdateParentCommand>
    {
        public UpdateParentCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber must be provided");
        }
    }
}
