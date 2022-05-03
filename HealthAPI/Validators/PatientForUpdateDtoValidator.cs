using FluentValidation;
using HealthAPI.Dtos;

namespace HealthAPI.Validators
{
    public class PatientForUpdateDtoValidator : AbstractValidator<PatientForUpdateDto>
    {
        public PatientForUpdateDtoValidator()
        {
            RuleFor(model => model.Address).NotEmpty().NotNull();
            RuleFor(model => model.UserId).NotEmpty().NotNull();
            RuleFor(model => model.FirstName).NotEmpty().NotNull();
            RuleFor(model => model.LastName).NotEmpty().NotNull();
            RuleFor(model => model.Gender).IsInEnum();
            RuleFor(model => model.DateOfBirth).NotEmpty().NotNull();
        }
    }
}
