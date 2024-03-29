﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalCMS.Repository;
using FinalCMS.ViewModel;
using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharPatientPrescriptionController : ControllerBase
    {
        private readonly IPharPatientPrescriptionRepository _patientPrescriptionRepository;

        // Construction Injection
        public PharPatientPrescriptionController(IPharPatientPrescriptionRepository patientPrescriptionRepository)
        {
            _patientPrescriptionRepository = patientPrescriptionRepository;
        }

        #region Get all Patient Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacistViewModel>>> GetAllPatientPrescriptions()
        {
            try
            {
                var patientPrescriptions = await _patientPrescriptionRepository.GetAllPatientPrescriptions();
                if (patientPrescriptions == null || patientPrescriptions.Count == 0)
                {
                    return NotFound("No Patient Prescriptions found");
                }
                return Ok(patientPrescriptions);
            }
            catch (Exception)
            {
                return BadRequest("Error while fetching Patient Prescriptions");
            }
        }
        #endregion


        #region Search Patient Prescriptions by PatientId
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PharmacistViewModel>>> SearchPatientPrescriptionsByPatientId([FromQuery] int? patientId)
        {
            try
            {
                var patientPrescriptions = await _patientPrescriptionRepository.SearchPatientPrescriptionsByPatientId(patientId);
                if (patientPrescriptions == null || patientPrescriptions.Count == 0)
                {
                    return NotFound($"No Patient Prescriptions found for PatientId '{patientId}'");
                }
                return Ok(patientPrescriptions);
            }
            catch (Exception)
            {
                return BadRequest("Error while searching Patient Prescriptions");
            }
        }
        #endregion

    }
}
