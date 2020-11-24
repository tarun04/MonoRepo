using FluentValidation;

namespace MonoRepo.Microservice.Tenant.Query.GetTenantByName
{
    public class GetTenantByNameQueryValidator : AbstractValidator<GetTenantByNameQuery>
    {
        public GetTenantByNameQueryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be provided.");
        }
    }
}
