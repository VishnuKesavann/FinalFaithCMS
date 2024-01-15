using FinalCMS.Doctor_Repository;
using FinalCMS.Doctor_Viewmodel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public readonly IDPatienthistoryRepository _patientHistoryRepository;


        public DAppointmentController(IDAppointmentRepository appointmentRepository, IDPatientviewRepository patientDetailsRepository, IDPatienthistoryRepository patientHistoryRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientDetailsRepository = patientDetailsRepository;
            _patientHistoryRepository = patientHistoryRepository;
            _patientHistoryRepository = patientHistoryRepository;
        }

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

       [HttpGet("GetPatientView")]
        public async Task<ActionResult<Patientview>> GetPatientView(int appointmentId)
        {
            var patientDetails = await _patientDetailsRepository.GetPatientViewAsync(appointmentId);

            if (patientDetails == null)
            {
                return NotFound(); // Or return an appropriate status code
            }

            return Ok(patientDetails);
        }



        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetPatientHistory(int patientId)
        {
            try
            {
                var patientHistory = await _patientHistoryRepository.GetPatientHistoryAsync(patientId);

                if (patientHistory == null || patientHistory.Count == 0)
                {
                    return NotFound($"No history found for patient with ID {patientId}");
                }

                return Ok(patientHistory);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
