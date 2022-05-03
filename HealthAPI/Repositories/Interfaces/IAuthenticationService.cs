using HealthAPI.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
