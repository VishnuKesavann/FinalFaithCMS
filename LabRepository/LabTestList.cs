using FinalCMS.LabViewModel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FinalCMS.LabRepository
{
    public class LabTestList : ILabTestList 
    {
        private readonly FinalCMS_dbContext _dbContext;

        public LabTestList(FinalCMS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabTestVM>> GetViewModelPrescriptions()
        {
            if (_dbContext != null)
            {
                // LINQ
                // Assuming you have a DataContext named "dbContext"

                var detailsQuery = from lp in _dbContext.LabPrescriptions
                                   join a in _dbContext.Appointment on lp.AppointmentId equals a.AppointmentId
                                   join p in _dbContext.Patient on a.PatientId equals p.PatientId
                                   join d in _dbContext.Doctor on a.DoctorId equals d.DoctorId
                                   join s in _dbContext.Staff on d.StaffId equals s.StaffId
                                   join l in _dbContext.Laboratory on lp.LabTestId equals l.TestId
                                   select new LabTestVM
                                   {   LabPrescId=lp.LabPrescriptionId,
                                       AppointmentId = a.AppointmentId,
                                       PatientName = p.PatientName,
                                       TestName = l.TestName,
                                       DoctorName = s.StaffName, // Changed from "staff_Name" to "fullName"
                                       LabTestStatus = lp.LabTestStatus
                                   };


                return await detailsQuery.ToListAsync();
            }

            return null;

        }

    }
}
