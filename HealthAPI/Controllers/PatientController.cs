using HealthAPI.Dtos;
using HealthAPI.Repositories.Interfaces;
using HealthAPI.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthAPI.Controllers
{
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PatientController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all the patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _service.PatientService.GetAllPatients(trackChanges: false);
            return Ok(patients);
        }

        /// <summary>
        /// Gets a single Patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name = "GetPatient")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            var patient = await _service.PatientService.GetPatient(id, trackChanges: false);
            return Ok(patient);
        }

        /// <summary>
        /// Creates a new Patient record
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientForCreationDto patient)
        {
            var patientCreationResponse = await _service.PatientService.AddPatient(patient, trackChanges: false);

            return CreatedAtRoute("GetPatient", new { id = patientCreationResponse.Id }, patientCreationResponse);
        }

        /// <summary>
        /// Get a collection of patients by there id's
        /// </summary>
        /// <param name="patientIds"></param>
        /// <returns></returns>
        [HttpGet("collection/({ids})", Name = "PatientCollection")]
        public async Task<IActionResult> GetPatientCollection(IEnumerable<Guid> patientIds)
        {
            var patients = await _service.PatientService.GetPatientByIds(patientIds, trackChanges: false);
            return Ok(patients);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreatePatientCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<PatientForCreationDto> patientCollection)
        {
            var result = await _service.PatientService.CreatePatientCollection(patientCollection);

            return CreatedAtRoute("PatientCollection", new { result.ids }, result.patients);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePatient(Guid patientId)
        {
            await _service.PatientService.DeletePatient(patientId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePatient(Guid patientId, [FromBody] PatientForUpdateDto patient)
        {
            await _service.PatientService.UpdatePatient(patientId: patientId, patientForUpdateDto: patient, trackChanges: false);
            return NoContent();
        }
    }
}
