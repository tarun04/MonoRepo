using FluentValidation;

namespace MonoRepo.Microservice.Application.Command.CommandHandlers.Parent.RemoveParent
{
    public class RemoveParentCommandValidator : AbstractValidator<RemoveParentCommand>
    {
        public RemoveParentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("ParentId must be provided");
        }
    }
}
