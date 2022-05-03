using HealthAPI.Data;
using HealthAPI.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly HealthAPIContext _healthAPIContext;
        private readonly Lazy<IPatientRepository> _patientRepository;
        private readonly Lazy<IUserRepository> _userRepository;

        public RepositoryManager(HealthAPIContext healthAPIContext)
        {
            _healthAPIContext = healthAPIContext;
            _patientRepository = new Lazy<IPatientRepository>(() => new PatientRepository(_healthAPIContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_healthAPIContext)); ;
        }

        public IPatientRepository Patient => _patientRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public async Task Save() => await _healthAPIContext.SaveChangesAsync();
    }
}
