using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.LabRepository
{
    public class LabreportRepository : ILabreportRepository
    {
        private readonly FinalCMS_dbContext _dbContext;


        public LabreportRepository (FinalCMS_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region get all LabReportGeneration
        public async Task<List<LabReportGeneration>> GetAllLabReportGeneration()
        {
            if (_dbContext != null)
            {
                return await _dbContext.LabReportGeneration.ToListAsync();
            }
            return null;
        }
        #endregion

        #region Add a LabReportGeneration
        public async Task<int> AddLabReport(LabReportGeneration labReport)
        {
            if (_dbContext != null)
            {
                await _dbContext.LabReportGeneration.AddAsync(labReport);
                await _dbContext.SaveChangesAsync();  // commit the transction
                return labReport.LabreportId;
            }
            return 0;
        }
        #endregion

        #region GetLabReportById
        public async Task<LabReportGeneration> GetLabReportById(int? id)
        {
            if (_dbContext != null)
            {
                var labReport = await _dbContext.LabReportGeneration.FindAsync(id);   //primary key
                return labReport;
            }
            return null;
        }
        #endregion

        #region get all BillGeneration
        public async Task<List<LabBillGeneration>> GetAllLabBillGeneration()
        {
            if (_dbContext != null)
            {
                return await _dbContext.LabBillGeneration.ToListAsync();
            }
            return null;
        }
        #endregion
    }
}
