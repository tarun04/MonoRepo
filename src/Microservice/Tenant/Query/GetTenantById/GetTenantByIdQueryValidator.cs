using FluentValidation;

namespace MonoRepo.Microservice.Tenant.Query.GetTenantById
{
    public class GetTenantByIdQueryValidator : AbstractValidator<GetTenantByIdQuery>
    {
        public GetTenantByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided.");
        }
    }
}
