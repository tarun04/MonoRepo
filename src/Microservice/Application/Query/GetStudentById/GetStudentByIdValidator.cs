using FluentValidation;

namespace MonoRepo.Microservice.Application.Query.GetStudentById
{
    public class GetStudentByIdValidator : AbstractValidator<GetStudentByIdQuery>
    {
        public GetStudentByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided.");
        }
    }
}
