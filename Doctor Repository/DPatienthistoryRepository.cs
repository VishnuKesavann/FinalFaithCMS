using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FinalCMS.Doctor_Viewmodel;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.Doctor_Repository
{
    public class DPatienthistoryRepository : IDPatienthistoryRepository
    {

        private readonly FinalCMS_dbContext _DbContext;
        public DPatienthistoryRepository(FinalCMS_dbContext DbContext)
        {
            _DbContext = DbContext;
        }


        public async Task<List<Patienthis>> GetPatientHistoryAsync(int patientId)
        {

            if (_DbContext != null)
            {
                var patientHistory = await _DbContext.Appointment
             .Include(a => a.Diagnosis)
                 .ThenInclude(d => d.PatientHistory)
             .Include(a => a.LabPrescriptions)
                 .ThenInclude(lp => lp.LabTest)
             .Include(a => a.MedicinePrescriptions)
                 .ThenInclude(mp => mp.PrescribedMedicine)
             .Include(a => a.LabReportGeneration)
                 .ThenInclude(rg => rg.Test)
             .Where(a => a.PatientId == patientId)
             .Select(a => new Patienthis
             {
                 AppointmentDate = a.AppointmentDate,
                 AppointmentId = a.AppointmentId,
                 Symptoms = a.Diagnosis.FirstOrDefault().Symptoms,
                 Diagnosis1 = a.Diagnosis.FirstOrDefault().Diagnosis1,
                 Note = a.Diagnosis.FirstOrDefault().Note,
                 MedicineName = a.MedicinePrescriptions.FirstOrDefault().PrescribedMedicine.MedicineName,
                 TestName = a.LabPrescriptions.FirstOrDefault().LabTest.TestName,
                 LabResult = a.LabReportGeneration.FirstOrDefault().LabResult ?? "N.A"
             })
             .ToListAsync();

                return patientHistory;



            }
            return null;

        }

    }
}
