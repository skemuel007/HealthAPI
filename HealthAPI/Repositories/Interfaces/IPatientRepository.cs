using HealthAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients(bool trackChanges);
        Task<Patient> GetPatient(Guid patientId, bool trackChanges);
        void AddPatient(Patient patient);
        Task<IEnumerable<Patient>> GetPatientsByIds(IEnumerable<Guid> patientIds, bool trackChanges);
        void DeletePatient(Patient patient);
    }
}
