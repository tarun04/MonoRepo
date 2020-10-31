using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Role Id must be provided.");
        }
    }
}
