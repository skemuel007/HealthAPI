using AutoMapper;
using HealthAPI.Dtos;
using HealthAPI.Models;
using HealthAPI.Repositories.Interfaces;
using HealthAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PatientService(IRepositoryManager repository,
            ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PatientDto> AddPatient(PatientForCreationDto patient, bool trackChanges)
        {
            // check user exists
            var user = await _repository.User.GetUser(patient.UserId, trackChanges);

            if ( user == null)
                throw new UserNotFoundException(userIdentifier: patient.UserId);

            var patientEntity = _mapper.Map<Patient>(patient);
            _repository.Patient.AddPatient(patientEntity);
            await _repository.Save();

            var createdPatientEntity = _mapper.Map<PatientDto>(patient);
            return createdPatientEntity;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients(bool trackChanges)
        {
            try
            {
                var patients = await _repository.Patient.GetAllPatients(trackChanges);
                /*var patientsDto = patients.Select(p => new PatientDto ( p.Id, p.UserId, p.FirstName, p.LastName,p.Address, p.Gender,
                    p.UserId, p.DateOfBirth)).ToList();*/
                var patientsDto = _mapper.Map<IEnumerable<PatientDto>>(patients);
                return patientsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllPatients)} service method {ex}");
                throw;
            }
        }

        public async Task<PatientDto> GetPatient(Guid patientId, bool trackChanges)
        {
            var patient = await GetPatientAndCheckIfItExists(patientId, trackChanges);

            if (patient is null)
                throw new PatientNotFoundException(patientId);

            var patientDto = _mapper.Map<PatientDto>(patient);
            return patientDto;
        }

        public async Task<IEnumerable<PatientDto>> GetPatientByIds(IEnumerable<Guid> patientIds, bool trackChanges)
        {
            if (patientIds == null)
                throw new IdParametersBadRequestException();

            var patientEntities = await _repository.Patient.GetPatientsByIds(patientIds, trackChanges);

            if (patientIds.Count() != patientEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var patients = _mapper.Map<IEnumerable<PatientDto>>(patientEntities);
            return patients;
        }

        public async Task<(IEnumerable<PatientDto> patients, string ids)> CreatePatientCollection(IEnumerable<PatientForCreationDto> patientCollection)
        {
            if (patientCollection == null)
                throw new CollectionBadRequest<IEnumerable<PatientForCreationDto>>(patientCollection);

            var patientEntities = _mapper.Map<IEnumerable<Patient>>(patientCollection);  
            foreach( var patient in patientEntities)
            {
                _repository.Patient.AddPatient(patient);
            }

            await _repository.Save();

            var patientCollectionCreated = _mapper.Map<IEnumerable<PatientDto>>(patientEntities);

            var patientIds = string.Join(",", patientCollectionCreated.Select(c => c.Id));

            return (patients: patientCollectionCreated, ids: patientIds);
        }
        public async Task DeletePatient(Guid patientId, bool trackChanges)
        {
            var patient = await GetPatientAndCheckIfItExists(patientId, trackChanges);
            if (patient is null)
                throw new PatientNotFoundException(patientId);

            _repository.Patient.DeletePatient(patient);
            await _repository.Save();
        }
        public async Task UpdatePatient(Guid patientId, PatientForUpdateDto patientForUpdateDto, bool trackChanges)
        {
            var user = await _repository.User.GetUser(patientForUpdateDto.UserId, trackChanges);

            if (user == null)
                throw new UserNotFoundException(userIdentifier: patientForUpdateDto.UserId);

            var patientEntity = await GetPatientAndCheckIfItExists(patientId, trackChanges);

            if (patientEntity is null)
                throw new PatientNotFoundException(patientId);

            _mapper.Map(patientForUpdateDto, patientEntity);
            await _repository.Save();
        }

        private async Task<Patient> GetPatientAndCheckIfItExists(Guid patientId, bool trackChanges)
        {
            var patient = await _repository.Patient.GetPatient(patientId, trackChanges);

            if (patient is null)
                throw new PatientNotFoundException(patientId);

            return patient;
        }
    }
}
