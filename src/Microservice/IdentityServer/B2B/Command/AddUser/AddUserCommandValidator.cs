using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name must be provided.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name must be provided.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be provided.");
            RuleFor(x => x.IsEnabled).NotEmpty().WithMessage("IsEnabled must be provided with a value.");
            RuleFor(x => x.Roles).NotEmpty().WithMessage("User must have at least 1 role.");
        }
    }
}
