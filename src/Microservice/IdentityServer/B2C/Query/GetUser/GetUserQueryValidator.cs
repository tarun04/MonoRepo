using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2C.Query.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant Id must be provided.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email must be valid.");
        }
    }
}
