using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public interface IMedicineRepository
    {
        Task<List<Medicine>> GetAllTblMedicines();
        Task<int> AddMedicine(Medicine medicine);
        Task UpdateMedicine(Medicine medicine);
        Task<Medicine> GetMedicineById(long? id);
        Task<int> DeleteMedicine(int? id);
    }
}
