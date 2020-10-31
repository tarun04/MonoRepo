using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.AddRole
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(c => c.RoleName).NotEmpty().WithMessage("Role Name must be provided.");
            RuleFor(c => c.Permissions).NotEmpty().WithMessage("Role must have at least 1 permission.");
        }
    }
}
