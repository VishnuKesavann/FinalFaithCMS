using FinalCMS.Doctor_Respository;
using FinalCMS.Doctor_ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DAppointmentController : ControllerBase
    {

        private readonly IDAppointmentRepository _appointmentRepository;
        public readonly IDPatientviewRepository _patientDetailsRepository;
       /* public readonly IPatientHistoryRepository _patientHistoryRepository;
*/

        public DAppointmentController(IDAppointmentRepository appointmentRepository, IDPatientviewRepository patientDetailsRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientDetailsRepository = patientDetailsRepository;
            //_patientHistoryRepository = patientHistoryRepository;

        }

        // List Appointments

        [HttpGet("GetAppointmentView")]
        public async Task<ActionResult<IEnumerable<Todayapp>>> GetAppointmentView(int docId)
        {
            var appointments = await _appointmentRepository.GetAppointmentViewAsync(docId);

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(appointments);
        }


        // View patient

        [HttpGet("GetPatientView")]
        public async Task<ActionResult<PatientDet>> GetPatientView(int appointmentId)
        {
            var patientDetails = await _patientDetailsRepository.GetPatientViewAsync(appointmentId);

            if (patientDetails == null)
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(patientDetails);


        }
    }
}
