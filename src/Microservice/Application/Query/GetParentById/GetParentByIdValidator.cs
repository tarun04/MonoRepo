using FluentValidation;

namespace MonoRepo.Microservice.Application.Query.GetParentById
{
    public class GetParentByIdValidator : AbstractValidator<GetParentByIdQuery>
    {
        public GetParentByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided.");
        }
    }
}
