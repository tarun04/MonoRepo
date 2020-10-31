using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Command.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
