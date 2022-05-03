using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IPatientRepository Patient { get; }
        IUserRepository User { get; }
        Task Save();
    }
}
