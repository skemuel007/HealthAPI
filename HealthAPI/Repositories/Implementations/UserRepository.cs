using HealthAPI.Data;
using HealthAPI.Models;
using HealthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Implementations
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(HealthAPIContext healthAPIContext) : base(healthAPIContext) { }

        public async Task<User> GetUser(string userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserByEmail(string email, bool trackChanges) =>
            await FindByCondition(u => u.Email == email, trackChanges)
            .SingleOrDefaultAsync();
    }
}
