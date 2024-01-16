using FinalCMS.LabViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.LabRepository
{
    public interface ILabTestList
    {
        Task<List<LabTestVM>> GetViewModelPrescriptions();
    }
}
