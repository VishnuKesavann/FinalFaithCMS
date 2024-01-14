using FinalCMS.Models;
using FinalCMS.Receptionist_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RPatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        public RPatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository; 
        }
        #region Get All Patient
        
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            return await _patientRepository.GetAllPatient();
        }
        #endregion
        #region AddAPatient
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
            #endregion

        }

    }

}
