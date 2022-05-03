using FluentValidation;
using HealthAPI.Dtos;

namespace HealthAPI.Validators
{
    public class PatientForCreationDtoValidator : AbstractValidator<PatientForCreationDto>
    {
        public PatientForCreationDtoValidator()
        {
            RuleFor(model => model.Address).NotEmpty().NotNull();
            RuleFor(model => model.UserId).NotEmpty().NotNull();
            RuleFor(model => model.FirstName).NotEmpty().NotNull();
            RuleFor(model => model.LastName).NotEmpty().NotNull();
            RuleFor(model => model.Gender).IsInEnum();
            RuleFor(model => model.DateOfBirth).NotEmpty().NotNull();
        }
    }

    public class UserForAuthenticationDtoValidator: AbstractValidator<UserForAuthenticationDto>
    {
        public UserForAuthenticationDtoValidator()
        {
            RuleFor(model => model.Password).NotEmpty().NotNull();
            RuleFor(model => model.UserName).NotEmpty().NotNull();
        }
    }
}
