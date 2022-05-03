using FluentValidation;
using HealthAPI.Dtos;
using HealthAPI.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAPI.Utils
{
    public static class FluentValidatorExtension
    {
        public static void ConfigureFluentValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<PatientForCreationDto>, PatientForCreationDtoValidator>();
            services.AddTransient<IValidator<PatientForUpdateDto>, PatientForUpdateDtoValidator>();
            services.AddTransient<IValidator<UserForRegistrationDto>, UserForRegistrationDtoValidator>();
        }
    }
}
