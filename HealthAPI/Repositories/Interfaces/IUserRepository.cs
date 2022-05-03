using HealthAPI.Models;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId, bool trackChanges);
        Task<User> GetUserByEmail(string email, bool trackChanges);
    }
}
