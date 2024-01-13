using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly FinalCMS_dbContext _Context;
        public MedicineRepository(FinalCMS_dbContext context)
        {
            _Context = context;
        }

        //Get all Medicine
        #region get all Medicine
        public async Task<List<Medicine>> GetAllTblMedicines()
        {
            if (_Context != null)
            {
                return await _Context.Medicine.ToListAsync();
            }
            return null;

        }
        #endregion

        #region Add a Medicine
        public async Task<int> AddMedicine(Medicine medicine)
        {
            if (_Context != null)
            {
                await _Context.Medicine.AddAsync(medicine);
                await _Context.SaveChangesAsync();  // commit the transction
                return (int)medicine.MedicineId;
            }
            return 0;
        }
        #endregion

        #region Update Medicine
        public async Task UpdateMedicine(Medicine medicine)
        {
            if (_Context != null)
            {
                _Context.Entry(medicine).State = EntityState.Modified;
                _Context.Medicine.Update(medicine);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetMedicineById
        public async Task<Medicine> GetMedicineById(long? id)
        {
            if (_Context != null)
            {
                var medicine = await _Context.Medicine.FindAsync(id);   //primary key
                return medicine;
            }
            return null;
        }
        #endregion

        #region Delete Medicine
        public async Task<int> DeleteMedicine(int? id)
        {
            if (_Context != null)
            {
                var med = await (_Context.Medicine.FirstOrDefaultAsync(emp => emp.MedicineId == id));

                if (med != null)
                {
                    //Delete
                    _Context.Medicine.Remove(med);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return (int)med.MedicineId;
                }
            }
            return 0;
        }
        #endregion
    }
}
