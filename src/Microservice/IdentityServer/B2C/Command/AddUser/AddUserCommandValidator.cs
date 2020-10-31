using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2C.Command.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name must be provided.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name must be provided.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number must be provided.");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Id must be provided.");
            RuleFor(x => x.SendConfirmationEmail).NotEmpty().WithMessage("SendConfirmationEmail must be provided with a value.");
        }
    }
}
