using FinalCMS.LabViewModel;
using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

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
                query = query.OrderByDescending(lr => lr.ReportDate);

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
                LabPrescriptions prescriptions = _dbContext.LabPrescriptions.FirstOrDefault(lb => lb.LabPrescriptionId == viewmodal.LabPrescId);
                prescriptions.LabTestStatus = "COMPLETED";
                _dbContext.SaveChangesAsync();
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

        #region Bill Generation
        public async Task<LabBillVM>GetBillVM(int LabreportId)
        {
            if (_dbContext != null)
            {
                var query = from l in _dbContext.LabReportGeneration
                            join a in _dbContext.Appointment on l.AppointmentId equals a.AppointmentId
                            join p in _dbContext.Patient on a.PatientId equals p.PatientId
                            join t in _dbContext.Laboratory on l.TestId equals t.TestId
                            where l.LabreportId == LabreportId
                            select new LabBillVM
                            {
                                
                                C=l.ReportDate,
                                AppointmentId = a.AppointmentId,
                                TestId = t.TestId,
                                TestName = t.TestName,
                                Amount = t.TestPrice,
                                TotalAmount = t.TestPrice + t.TestPrice * 0.18m,                       
                                PatientId = p.PatientId,
                                PatientName=p.PatientName,
                                LabreportId = l.LabreportId,
                            };
                LabBillVM result = await query.FirstOrDefaultAsync();

                if (result != null)
                {
                    var Bill =await AddLabBillGeneration(result);
                    result.LabbillId=Bill.LabbillId;
                }


                return await query.FirstOrDefaultAsync();
            }
            return null;

        }
        #endregion
        public async Task< LabBillGeneration> AddLabBillGeneration(LabBillVM labBillVM)
        {
            LabBillGeneration existingLabBill = _dbContext.LabBillGeneration.FirstOrDefault(lb => lb.LabreportId == labBillVM.LabreportId);

            if (existingLabBill != null)
            {
                // Report ID already exists, return the existing record
                return existingLabBill;
            }
            LabBillGeneration newLabBill = new LabBillGeneration
            {
                LabbillId = labBillVM.LabbillId,
                C = DateTime.Now, // Assuming you want to set the current date and time
                TotalAmount = labBillVM.TotalAmount,
                Amount = labBillVM.Amount,
                PatientId = labBillVM.PatientId,
                TestId = labBillVM.TestId,
                LabreportId = labBillVM.LabreportId,
                AppointmentId = labBillVM.AppointmentId
            };

            _dbContext.LabBillGeneration.Add(newLabBill);
            await _dbContext.SaveChangesAsync();

            return newLabBill;
        }

    }
}
