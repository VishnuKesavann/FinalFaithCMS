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
    }
}
