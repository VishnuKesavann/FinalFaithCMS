using FinalCMS.Models;
using FinalCMS.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCMS.Repository
{
    public class PharPatientPrescriptionRepository : IPharPatientPrescriptionRepository
    {
        private readonly FinalCMS_dbContext _context;
        private object _Context;

        public PharPatientPrescriptionRepository(FinalCMS_dbContext context)
        {
            _context = context;
        }

        public async Task<List<PharmacistViewModel>> GetAllPatientPrescriptions()
        {
            if (_context != null)
            {
                var patientPrescriptions = await (
                    from appointment in _context.Appointment
                    join patient in _context.Patient on appointment.PatientId equals patient.PatientId
                    join prescription in _context.MedicinePrescriptions on appointment.AppointmentId equals prescription.AppointmentId
                    select new PharmacistViewModel
                    {
                        PatientId = patient.PatientId,
                        PatientName = patient.PatientName,
                        PhoneNumber = patient.PhoneNumber,
                        PrescribedMedicine = prescription.PrescribedMedicine.MedicineName,
                        Dosage = prescription.Dosage,
                        DosageDays = prescription.DosageDays,
                        Quantity = prescription.MedicineQuantity
                    }).ToListAsync();

                return patientPrescriptions;
            }

            return null;
        }


        public async Task<List<PharmacistViewModel>> SearchPatientPrescriptionsByPatientId(int? patientId)
        {
            if (_context != null && patientId.HasValue)
            {
                var patientPrescriptions = await (
                    from appointment in _context.Appointment
                    join patient in _context.Patient on appointment.PatientId equals patient.PatientId
                    join prescription in _context.MedicinePrescriptions on appointment.AppointmentId equals prescription.AppointmentId
                    where patient.PatientId == patientId
                    select new PharmacistViewModel
                    {
                        PatientId = patient.PatientId,
                        PatientName = patient.PatientName,
                        PhoneNumber = patient.PhoneNumber,
                        PrescribedMedicine = prescription.PrescribedMedicine.MedicineName,
                        Dosage = prescription.Dosage,
                        DosageDays = prescription.DosageDays,
                        Quantity = prescription.MedicineQuantity
                    }).ToListAsync();

                return patientPrescriptions;
            }

            return null;
        }

        
    }
}
