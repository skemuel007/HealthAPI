using HealthAPI.Dtos;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string userId, bool trackChanges);
        Task<UserDto> GetUserByEmail(string email, bool trackChanges);
    }
}
