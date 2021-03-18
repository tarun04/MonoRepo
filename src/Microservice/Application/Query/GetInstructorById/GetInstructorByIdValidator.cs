using FluentValidation;

namespace MonoRepo.Microservice.Application.Query.GetInstructorById
{
    public class GetInstructorByIdValidator : AbstractValidator<GetInstructorByIdQuery>
    {
        public GetInstructorByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided.");
        }
    }
}
