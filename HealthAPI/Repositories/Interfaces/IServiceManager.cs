namespace HealthAPI.Repositories.Interfaces
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IPatientService PatientService { get; }
        IUserService UserService { get; }
    }
}
