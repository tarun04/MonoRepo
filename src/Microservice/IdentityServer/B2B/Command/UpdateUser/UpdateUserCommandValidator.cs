using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User Id must be provided.");
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).WithMessage("First name must have at least 2 characters");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).WithMessage("Last name must have at least 2 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be provided.");
            RuleFor(x => x.IsEnabled).NotEmpty().WithMessage("IsEnabled must be provided with value.");
            RuleFor(x => x.Username).NotEmpty().MinimumLength(2).MaximumLength(256).WithMessage("Username must have at least 2 characters");
            RuleFor(x => x.Roles).NotEmpty().WithMessage("User must have at least 1 role.");
        }
    }
}
