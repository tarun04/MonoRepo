using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(X => X.UserId).NotEmpty().WithMessage("User Id must be provided.");
        }
    }
}
