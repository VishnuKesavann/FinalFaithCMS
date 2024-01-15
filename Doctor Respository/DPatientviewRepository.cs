using FinalCMS.Doctor_ViewModel;
using FinalCMS.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.Doctor_Respository
{
    public class DPatientviewRepository : IDPatientviewRepository
    {

        private readonly FinalCMS_dbContext _dbcontext;
        public DPatientviewRepository(FinalCMS_dbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<PatientDet> GetPatientViewAsync(int appointmentId)
        {
            using (var context = new FinalCMS_dbContext()) // Replace YourDbContext with the actual name of your DbContext
            {
                var query = from patient in context.Patient
                            join appointment in context.Appointment on patient.PatientId equals appointment.PatientId
                            where appointment.AppointmentId == appointmentId
                            select new PatientDet
                            {
                                AppointmentId = appointment.AppointmentId,
                                PatientName = patient.PatientName,
                                Gender = patient.Gender,
                                BloodGroup = patient.BloodGroup,
                                PhoneNumber = patient.PhoneNumber,
                                CheckUpStatus = appointment.CheckUpStatus,
                                PatientAge = (int)Math.Floor((DateTime.Now - patient.PatientDob).TotalDays / 365)
                            };

                return await query.FirstOrDefaultAsync();
            }
        }

    }
}
