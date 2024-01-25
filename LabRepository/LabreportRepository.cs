using FinalCMS.LabViewModel;
using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace FinalCMS.LabRepository
{
    public class LabreportRepository : ILabreportRepository
    {
        private readonly FinalCMS_dbContext _dbContext;


        public LabreportRepository (FinalCMS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabReportVM>> GetViewModelReport()
        {
            if (_dbContext != null)
            {
                var query = from lr in _dbContext.LabReportGeneration
                            join l in _dbContext.Laboratory on lr.TestId equals l.TestId
                            join a in _dbContext.Appointment on lr.AppointmentId equals a.AppointmentId
                            join p in _dbContext.Patient on a.PatientId equals p.PatientId

                            select new LabReportVM
                            {
                                ReportDate = lr.ReportDate,
                                ReportId = lr.LabreportId,
                                PatientName = p.PatientName,
                                TestName = l.TestName,
                                HighRange = l.HighRange,
                                LowRange = l.LowRange,
                                TestResult = lr.LabResult,
                                Remarks = lr.Remarks
                            };

                return await query.ToListAsync();
            }
            return null;
        }
        #region 
        public async Task<int> AddReport(LabReportVM viewmodal)
        {
            if (_dbContext != null)
            {
                LabReportGeneration report = new LabReportGeneration()
                {
                    AppointmentId = viewmodal.AppointmentId,
                    LabResult = viewmodal.LabResult,
                    ReportDate = viewmodal.ReportDate,
                    Remarks = viewmodal.Remarks,
                    LabPrescId = viewmodal.LabPrescId,
                    TestId = viewmodal.TestId,
                    StaffId = viewmodal.StaffId
                };

                await _dbContext.LabReportGeneration.AddAsync(report);
                await _dbContext.SaveChangesAsync();
                return report.LabreportId;
            }
            return 0;
        }
        #endregion

        #region GET
        public async Task<GetIDVM> GetIDViewModel(int labpresId)
        {
            if (_dbContext != null)
            {
                var query = from lr in _dbContext.LabPrescriptions
                            join a in _dbContext.Appointment on lr.AppointmentId equals a.AppointmentId
                            join p in _dbContext.Doctor on a.DoctorId equals p.DoctorId
                            join s in _dbContext.Staff on p.StaffId equals s.StaffId
                            where lr.LabPrescriptionId == labpresId
                            select new GetIDVM
                            {
                                LabPrescId=lr.LabPrescriptionId,
                                AppointmentId = lr.AppointmentId,
                                TestId = lr.LabTestId,
                                StaffId = s.StaffId,

                            };

                return await query.FirstOrDefaultAsync();
            }
            return null;
        }
        #endregion

    }
}
