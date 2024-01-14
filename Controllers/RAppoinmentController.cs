using FinalCMS.Models;
using FinalCMS.Receptionist_Repository;
using FinalCMS.Receptionist_ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAppoinmentController : ControllerBase
    { private readonly IAppointmentRepository _appointmentRepository;
        public RAppoinmentController(IAppointmentRepository appointmentRepository)
        {
                _appointmentRepository = appointmentRepository;
        }
        #region GetAllDepartment
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            return await _appointmentRepository.GetAllDepartment();
        }
        #endregion
        #region Get all specialization by departmentId
        [HttpGet("GetSpecializationByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetAllSpecializaitonByDepartmentId(int? departmentId)
        {
            var specialization = await _appointmentRepository.GetAllSpecializationByDepartmentId(departmentId);
            if (specialization != null && specialization.Count > 0)
            {
                return Ok(specialization);
            }
            else 
            {
                return NotFound();
            }

        }

        #endregion
        #region Get All Doctors by SpecializationId
        [HttpGet("GetDoctorBySpecializationId/{specializationId}")]
        public async Task<ActionResult<IEnumerable<DoctorViewModel>>> GetAllDoctorsBySpecializationId(int? specializationId) 
        {
            var doctor = await _appointmentRepository.GetAllDoctorBySpecializationId(specializationId);
            if (doctor != null && doctor.Count > 0)
            {
                return Ok(doctor);
            }
            else 
            {
                return NotFound();
            }
        }

        #endregion
    }
}
