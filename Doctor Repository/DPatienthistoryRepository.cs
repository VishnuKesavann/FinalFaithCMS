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
           
                var query = from appointment in _DbContext.Appointment
                            join diagnosis in _DbContext.Diagnosis on appointment.AppointmentId equals diagnosis.AppointmentId
                            join labPrescription in _DbContext.LabPrescriptions on diagnosis.AppointmentId equals labPrescription.AppointmentId
                            join labTest in _DbContext.Laboratory on labPrescription.LabTestId equals labTest.TestId
                            join medicinePrescription in _DbContext.MedicinePrescriptions on diagnosis.AppointmentId equals medicinePrescription.AppointmentId
                            join medicine in _DbContext.Medicine on medicinePrescription.MedPrescriptionId equals medicine.MedicineId
                            join reportGeneration in _DbContext.LabReportGeneration on medicinePrescription.AppointmentId equals reportGeneration.AppointmentId into rg
                            from report in rg.DefaultIfEmpty()
                            where appointment.PatientId == patientId
                            select new Patienthis
                            {
                                AppointmentDate = appointment.AppointmentDate,
                                Symptoms = diagnosis.Symptoms,
                                Diagnosis1 = diagnosis.Diagnosis1,
                                Note = diagnosis.Note,
                                MedicineName = medicine.MedicineName,
                                TestName = labTest.TestName, // Use the TestName from TblLabTests
                                LabResult = report != null ? report.LabResult : "N.A"
                            };

                return await query.ToListAsync();

            
        }

    }
}
