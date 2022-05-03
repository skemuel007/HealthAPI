using HealthAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatients(bool trackChanges);
        Task<PatientDto> GetPatient(Guid patientId, bool trackChanges);
        Task<PatientDto> AddPatient(PatientForCreationDto patient, bool trackChanges);
        Task<IEnumerable<PatientDto>> GetPatientByIds(IEnumerable<Guid> patientIds, bool trackChanges);
        Task<(IEnumerable<PatientDto> patients, string ids)> CreatePatientCollection(IEnumerable<PatientForCreationDto> patientCollection);
        Task DeletePatient(Guid patientId, bool trackChanges);
        Task UpdatePatient(Guid patientId, PatientForUpdateDto patientForUpdateDto, bool trackChanges);
    }
}
