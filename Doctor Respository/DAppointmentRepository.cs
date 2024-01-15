using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalCMS.Models;
using System.Linq;
using FinalCMS.Doctor_ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.Doctor_Respository
{
    public class DAppointmentRepository
    {

        private readonly FinalCMS_dbContext _dbContext;
        public DAppointmentRepository(FinalCMS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region GetPatientAppointments

        public async Task<List<Todayapp>> GetAppointmentViewAsync(int docId)
        {
            using (var context = new FinalCMS_dbContext()) // Replace YourDbContext with the actual name of your DbContext
            {
                var todayDate = DateTime.Now.Date;

                var query = from patient in context.Patient
                            join appointment in context.Appointment on patient.PatientId equals appointment.PatientId
                            where appointment.DoctorId == docId && appointment.AppointmentDate.Date == todayDate
                            select new Todayapp
                            {
                                TokenNo = appointment.TokenNo,
                                PatientName = patient.PatientName,
                                Gender = patient.Gender,
                                PatientAge = (int)Math.Floor((todayDate - patient.PatientDob).TotalDays / 365),
                                CheckUpStatus = appointment.CheckUpStatus,
                                AppointmentId = appointment.AppointmentId
                            };

                return await query.ToListAsync();
            }
        }

        #endregion

    }
}
