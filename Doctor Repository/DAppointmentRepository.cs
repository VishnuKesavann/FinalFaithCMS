using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalCMS.Models;
using FinalCMS.Doctor_Viewmodel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.Doctor_Repository
{
    public class DAppointmentRepository : IDAppointmentRepository
    {


        private readonly FinalCMS_dbContext _dbcontext;
        public DAppointmentRepository(FinalCMS_dbContext dbcontext)
        {
            _dbcontext = dbcontext;
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
                                PatientId = patient.PatientId,
                                DoctorId = appointment.DoctorId,
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
