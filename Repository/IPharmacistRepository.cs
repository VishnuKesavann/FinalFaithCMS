using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Repository
{
    public interface IPharmacistRepository
    {
        Task<List<Medicine>> GetAllMedicine();
        Task<Medicine> GetDetailsById(int? id);
        Task<List<Medicine>> SearchMedicineByName(string name);
    }
}
