using HealthAPI.Data;
using HealthAPI.Models;
using HealthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Implementations
{
    internal sealed class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(HealthAPIContext healthAPIContext)
            : base(healthAPIContext) { }

        public void AddPatient(Patient patient)
        {
            Create(patient);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToListAsync();

        public async Task<Patient> GetPatient(Guid patientId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(patientId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Patient>> GetPatientsByIds(IEnumerable<Guid> patientIds, bool trackChanges) =>
            await FindByCondition(p => patientIds.Contains(p.Id), trackChanges)
            .ToListAsync();

        public void DeletePatient(Patient patient) => Delete(patient);

    }
}
