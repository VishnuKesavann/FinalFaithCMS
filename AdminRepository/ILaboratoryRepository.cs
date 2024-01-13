using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public interface ILaboratoryRepository
    {
        Task<List<Laboratory>> GetAllTblLabTests();
        Task<int> AddLabtest(Laboratory lab);
        Task UpdateLabTest(Laboratory lab);
        Task<Laboratory> GetLabById(int? id);
        Task<int> DeleteLabTest(int? id);

    }
}
