﻿using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCMS.Receptionist_Repository
{
    public class PatientRepository:IPatientRepository
    {
        private readonly FinalCMS_dbContext _context;
        public PatientRepository(FinalCMS_dbContext context)
        {
           _context = context;
        }
        #region Get All Active Patient Records
        public async Task<List<Patient>> GetAllPatient()
        {
            if (_context != null)
            {
                return await _context.Patient.Where(p=>p.PatientStatus=="ACTIVE").ToListAsync();
            }
            return null;
        }
        #endregion
        #region Add an Patient
        //add a Patient
        public async Task<int> AddPatient(Patient patient)
        {
            if (_context != null)
            {
                await _context.Patient.AddAsync(patient);
                await _context.SaveChangesAsync();
                return patient.PatientId; ;
            }
            return 0;
        }
        #endregion
        #region Update a Patient
        public async Task<Patient> UpdatePatient(Patient patient)
        {
            if (_context!=null)
            {
                _context.Entry(patient).State = EntityState.Modified;
                _context.Patient.Update(patient);
                await _context.SaveChangesAsync();
                return patient;
            }
            return null;
        }
        #endregion
        #region Get Patient By Id
        public async Task<Patient> GetPatientById(int? id)
        {
            if (_context!=null)
            {
                var patient = await _context.Patient.FindAsync(id);
                return patient;
            }
            return null;
        }
        #endregion
        #region Disable Patient Status
        public async Task<Patient> DisableStatus(int? paitientId)
        {
            var patient = await _context.Patient.FindAsync(paitientId);
            if (patient != null)
            {
                patient.PatientStatus = "DISABLED";
                await _context.SaveChangesAsync();
            }
            return patient;
        }
        #endregion

        #region Get All Disabled Patient Records
        public async Task<List<Patient>> GetAllDisabledPatients()
        {
            if (_context != null)
            {
                return await _context.Patient.Where(p => p.PatientStatus == "DISABLED").ToListAsync();
            }
            return null;
        }
        #endregion
        #region Enable Patient Status
        public async Task<Patient> EnableStatus(int? paitientId)
        {
            var patient = await _context.Patient.FindAsync(paitientId);
            if (patient != null)
            {
                patient.PatientStatus = "ACTIVE";
                await _context.SaveChangesAsync();
            }
            return patient;
        }
        #endregion
        #region serach of Active patient Records
        public async Task<List<Patient>> searchFilterPatients(string RegisterNumber=null,long phoneNumber = 0)
        {
            IQueryable<Patient> query = _context.Patient;
            if (!string.IsNullOrEmpty(RegisterNumber))
            {
                query=query.Where(p=>EF.Functions.Like(p.RegisterNo,$"%{RegisterNumber}%") && p.PatientStatus=="ACTIVE");
            }
            else if(phoneNumber > 0)
            {
                string phoneNumberString = phoneNumber.ToString();
                query =query.Where(p=>EF.Functions.Like(p.PhoneNumber.ToString(),$"%{phoneNumberString}%")&& p.PatientStatus=="ACTIVE");
            }
            return await query.ToListAsync();
        }
        #endregion
        #region search of Disabled Patient Records
        public async Task<List<Patient>> searchFilterDisabledPatients(string RegisterNumber=null,long phoneNumber = 0)
        {
            IQueryable<Patient> query= _context.Patient;
            if (!string.IsNullOrEmpty(RegisterNumber))
            {
                query = query.Where(p => p.RegisterNo == RegisterNumber && p.PatientStatus == "DISABLED");
            }
            else if (phoneNumber > 0)
            {
                query = query.Where(p => p.PhoneNumber == phoneNumber && p.PatientStatus == "DISABLED");
            }
            return await query.ToListAsync();
        }
        #endregion
    }
}
