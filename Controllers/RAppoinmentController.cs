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
{   /// <summary>
    /// Controller for managing receptionist appointments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RAppoinmentController : ControllerBase
    { private readonly IAppointmentRepository _appointmentRepository;
        /// <summary>
        /// Controller for managing receptionist appointments constructor.
        /// </summary>
        public RAppoinmentController(IAppointmentRepository appointmentRepository)
        {
                _appointmentRepository = appointmentRepository;
        }
        #region GetAllDepartment
        /// <summary>
        /// Gets all departments.
        /// </summary>
        /// <returns>List of departments.</returns>

        [HttpGet]
        [Route("GetAllDepartments")]
        
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            return await _appointmentRepository.GetAllDepartment();
        }
        #endregion
        #region Get all specialization by departmentId
        /// <summary>
        /// Gets all specializations based on the department Id.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>Action result with the list of specializations.</returns>
        [HttpGet("GetSpecializationByDepartmentId/{departmentId}")]
        //to get all specialization based on the department Id
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
        /// <summary>
        /// Gets all doctor based on the specialization Id.
        /// </summary>
        /// <param name="specializationId">The specialization identifier.</param>
        /// <returns>Action result with the list of doctors.</returns>
        [HttpGet("GetDoctorBySpecializationId/{specializationId}")]
        // to get all doctors by specializationId
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
        /// <summary>
        /// Books a new appointment and generates a bill.
        /// </summary>
        /// <param name="appointment_ViewModel">The view model containing appointment details.</param>
        /// <param name="isNewPatient">A flag indicating whether the patient is a new patient.</param>
        /// <returns>Action result indicating the result of the booking operation.</returns>
        [HttpPost("BookAppointment")]
        //to book appointment and Generate Bill
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
        /// <summary>
        /// Retrieves the details of a bill by bill Id.
        /// </summary>
        /// <param name="billId">The identifier of the bill.</param>
        /// <returns>
        /// ActionResult containing the bill details if found, 
        /// NotFoundResult if the bill is not found, 
        /// or BadRequestResult if an exception occurs during the retrieval.
        /// </returns>
        [HttpGet("GetBillDetails/{billId}")]
        //to get the bill details by bill Id
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
        /// <summary>
        /// Gets all Appointments.
        /// </summary>
        /// <returns>List of appointments.</returns>
        [HttpGet("GetAllAppointments")]

        //to get all appointments in the clinic
        public async Task<IEnumerable<BillViewModel>> GetAllAppointments() 
        {
            return await _appointmentRepository.GetAllAppointmentsWithBillViewModel(); 
        }
        #endregion
        #region Get AppointmentDetails by appointment Id
        /// <summary>
        /// Retrieves the details of an appointment by appointment Id.
        /// </summary>
        /// <param name="appointmentId">The identifier of the appointment.</param>
        /// <returns>
        /// ActionResult containing the appointment details if found, 
        /// NotFoundResult if the appointment is not found, 
        /// or BadRequestResult if an exception occurs during the retrieval.
        /// </returns>
        [HttpGet("GetAppointment/{appointmentId}")]
        //to get the appointment details by appointment Id
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
        /// <summary>
        /// Cancels the appointment by appointment Id.
        /// </summary>
        /// <param name="appointmentId">The appointment identifier.</param>
        /// <returns>ActionResult indicating the result of the cancellation.</returns>
        [HttpPatch("cancelAppointment/{appointmentId}")]
        // to cancel the appointment by appointment Id
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
