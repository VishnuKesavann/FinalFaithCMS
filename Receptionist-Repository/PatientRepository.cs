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
    }
}
