using FinalCMS.Models;
using FinalCMS.Receptionist_Repository;
using FinalCMS.Receptionist_ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        #region Book Appointment And Generate Bill
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment_ViewModel appointment_ViewModel,bool isNewPatient)
        {
            try
            {
                var bookNewAppointment = await _appointmentRepository.BookAppointment(appointment_ViewModel,isNewPatient);
                return Ok(bookNewAppointment);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion
        #region Display  the Bill Details
        [HttpGet("GetBillDetails/{billId}")]
        public async Task<IActionResult> BillGeneration(int? billId) 
        {
            try
            {
                var billgenerated= await _appointmentRepository.BillDetails(billId);
                if (billgenerated!=null)
                {
                    return Ok(billgenerated);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        #endregion
        #region Get All Appointments
        [HttpGet("GetAllAppointments")]
        public async Task<IEnumerable<BillViewModel>> GetAllAppointments() 
        {
            return await _appointmentRepository.GetAllAppointmentsWithBillViewModel(); 
        }
        #endregion
        #region Get AppointmentDetails by appointment Id
        [HttpGet("GetAppointment/{appointmentId}")]
        public async Task<ActionResult<BillViewModel>> GetAppointmentDetailsByAppointmentId(int? appointmentId) 
        {
            try
            {
                var appointment= await _appointmentRepository.GetAppointmentDetailsById(appointmentId);
                if (appointment!=null)
                {
                    return Ok(appointment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"error:{ex.Message}");
                
            }
        }
        #endregion
        #region CANCEL APPOINTMENT
        [HttpPatch("cancelAppointment/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int? appointmentId) 
        {
            try
            {
                var appointment= await _appointmentRepository.CancelAppointment(appointmentId);
                if (appointment!=null)
                {
                    return Ok(appointment);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
        }
        #endregion
    }
}
