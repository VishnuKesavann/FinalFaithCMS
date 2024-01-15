using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCMS.Repository
{
    public class PharmacistRepository: IPharmacistRepository
    {
        private readonly FinalCMS_dbContext _Context;

        public PharmacistRepository(FinalCMS_dbContext context)
        {
            _Context = context;
        }

        #region Get all Details
        public async Task<List<Medicine>> GetAllMedicine()
        {
            if (_Context != null)
            {
                return await _Context.Medicine.ToListAsync();
            }
            return null;
        }
        #endregion

        #region GetDetailsById
        public async Task<Medicine> GetDetailsById(int? id)
        {
            if (_Context != null)
            {
                var med = await _Context.Medicine.FindAsync(id);   //primary key
                return med;
            }
            return null;
        }
        #endregion

        #region Search Medicine By Name
        public async Task<List<Medicine>> SearchMedicineByName(string name)
        {
            if (_Context != null)
            {
                return await _Context.Medicine
                    .Where(m => m.MedicineName.Contains(name))
                    .ToListAsync();
            }
            return null;
        }
        #endregion

    }
}
