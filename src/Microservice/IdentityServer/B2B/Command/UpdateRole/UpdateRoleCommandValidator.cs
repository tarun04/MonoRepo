using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Role Id must be provided.");
            RuleFor(x => x.RoleName).NotEmpty().MinimumLength(2).MaximumLength(256).WithMessage("Role name should have at least 2 characters.");
        }
    }
}
