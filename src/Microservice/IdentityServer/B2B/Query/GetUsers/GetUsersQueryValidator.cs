using FluentValidation;

namespace MonoRepo.Microservice.IdentityServer.B2B.Query.GetUsers
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.PermissionType).NotEmpty().WithMessage("Permission type must be provided.");
            RuleFor(x => x.ClaimName).NotEmpty().WithMessage("Claim name must be provided.");
        }
    }
}
