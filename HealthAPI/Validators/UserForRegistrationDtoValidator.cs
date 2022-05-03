using FluentValidation;
using HealthAPI.Dtos;

namespace HealthAPI.Validators
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(model => model.Password).NotEmpty().NotNull();
            RuleFor(model => model.Roles).NotEmpty().NotNull();
            RuleFor(model => model.FirstName).NotEmpty().NotNull();
            RuleFor(model => model.LastName).NotEmpty().NotNull();
            RuleFor(model => model.PhoneNumber).NotNull().NotEmpty();
            RuleFor(model => model.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
