using AutoMapper;
using HealthAPI.Models;
using HealthAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace HealthAPI.Repositories.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IPatientService> _patientService;
        private readonly Lazy<IUserService> _userService;
    
        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger, IMapper mapper, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger: logger, mapper: mapper, userManager: userManager, configuration: configuration));
            _patientService = new Lazy<IPatientService>(() => new PatientService(repositoryManager, logger, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IPatientService PatientService => _patientService.Value;
        public IUserService UserService => _userService.Value;
    }
}
