using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.LabRepository
{
    public interface ILabreportRepository
    {
        Task<List<LabReportGeneration>> GetAllLabReportGeneration();

        Task<int> AddLabReport(LabReportGeneration labReport);

        Task<LabReportGeneration> GetLabReportById(int? id);

        Task<List<LabBillGeneration>> GetAllLabBillGeneration();
    }
}
