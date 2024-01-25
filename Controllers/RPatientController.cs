using FinalCMS.Models;
using FinalCMS.Receptionist_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    /// <summary>
    /// Controller for managing pantient Records.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RPatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        /// <summary>
        /// Controller for managing patients records constructor.
        /// </summary>
        public RPatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository; 
        }
        #region Get All Patient
        /// <summary>
        /// Gets all patients.
        /// </summary>
        /// <returns>List of patients.</returns>

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            return await _patientRepository.GetAllPatient();
        }
        #endregion
        #region AddAPatient
        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient information to be added.</param>
        /// <returns>
        /// IActionResult containing the patientId if the patient is added successfully, 
        /// NotFoundResult if the patient addition is not successful, 
        /// or BadRequestResult if there are validation errors or an exception occurs during the process.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Patient patient)
        {   //Check  the validation of the body
            if (ModelState.IsValid)
            {
                try
                {
                    var patientId = await _patientRepository.AddPatient(patient);
                    if (patientId > 0)
                    {
                        return Ok(patientId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);

                }

            }
            return BadRequest();//if the above does not  work 
        }
        #endregion
        #region Update a Patient
        /// <summary>
        /// Updates patient information.
        /// </summary>
        /// <param name="patient">The updated patient information.</param>
        /// <returns>
        /// IActionResult containing the updated patient information if the update is successful, 
        /// or BadRequestResult if there are validation errors or an exception occurs during the update.
        /// </returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] Patient patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _patientRepository.UpdatePatient(patient);
                    return Ok(patient);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }
        #endregion
        #region Get Patient By Id
        /// <summary>
        /// Gets all doctor based on the patient Id.
        /// </summary>
        /// <param name="id">The patient identifier.</param>
        /// <returns>Action result with the list of patients.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int? id) 
        {
            try
            {
                var patient = await _patientRepository.GetPatientById(id);
                if (patient == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patient);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Patient: {ex.Message}");
            }

        }
        #endregion
        #region Disable PatientRecords
        /// <summary>
        /// Disable the patient records by patient Id.
        /// </summary>
        /// <param name="patientId">The appointment identifier.</param>
        /// <returns>ActionResult indicating the result of the cancellation.</returns>
        [HttpPatch("{patientId}")]
        public async Task<IActionResult> Disable(int patientId)
        {
            try
            {
                var patient = await _patientRepository.DisableStatus(patientId);
                if (patient == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patient);
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Disable: {ex.Message}");
            }
            
        }
        #endregion
        #region Get All Disabled Patient Records
        /// <summary>
        /// Gets all Disabled patients.
        /// </summary>
        /// <returns>List of disabled patients records.</returns>
        [HttpGet]
        [Route("GetDisabledPatient")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetDisabledPatient()
        {
            return await _patientRepository.GetAllDisabledPatients();
        }
        #endregion

        #region Enable PatientRecords
        /// <summary>
        /// Enable the patient records by patient Id.
        /// </summary>
        /// <param name="patientId">The appointment identifier.</param>
        /// <returns>ActionResult indicating the result of the cancellation.</returns>
        [HttpPatch("Enable/{patientId}")]
        public async Task<IActionResult> Enable(int patientId)
        {
            try
            {
                var patient = await _patientRepository.EnableStatus(patientId);
                if (patient == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(patient);
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Enable: {ex.Message}");
            }

        }
        #endregion
        #region Search patient records
        [HttpGet]
        [Route("searchPatients")]
        public async Task<IActionResult> SearchPatients(string registerNumber=null,long phoneNumber = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(registerNumber) && phoneNumber==0) 
                {
                    return BadRequest("Please provide either Register Number or Phone Number");
                }
                var patients = await _patientRepository.searchFilterPatients(registerNumber, phoneNumber);
                if (patients == null || patients.Count == 0)
                {
                    return NotFound("No patients found");
                }
                return Ok(patients);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error");
                
            }
            
        }
        #endregion
        #region Search Disabled patient records
        [HttpGet]
        [Route("DisabledsearchPatients")]
        public async Task<IActionResult> DisabledSearchPatients(string registerNumber = null, long phoneNumber = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(registerNumber) && phoneNumber == 0)
                {
                    return BadRequest("Please provide either Register Number or Phone Number");
                }
                var patients = await _patientRepository.searchFilterDisabledPatients(registerNumber, phoneNumber);
                if (patients == null || patients.Count == 0)
                {
                    return NotFound("No patients found");
                }
                return Ok(patients);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server error");

            }

        }
        #endregion
    }

}
