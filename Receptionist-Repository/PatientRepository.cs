using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Patient>> GetAllPatient()
        {
            if (_context != null)
            {
                return await _context.Patient.ToListAsync();
            }
            return null;
        }
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
    }
}
