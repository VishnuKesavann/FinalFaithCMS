using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public class LaboratoryRepository : ILaboratoryRepository
    {
        private readonly FinalCMS_dbContext _Context;
        public LaboratoryRepository(FinalCMS_dbContext context)
        {
            _Context = context;
        }
        #region get all Labtests
        public async Task<List<Laboratory>> GetAllTblLabTests()
        {
            if (_Context != null)
            {
                return await _Context.Laboratory.ToListAsync();
            }
            return null;

        }
        #endregion

        #region Add a Labtests
        public async Task<int> AddLabtest(Laboratory lab)
        {
            if (_Context != null)
            {
                await _Context.Laboratory.AddAsync(lab);
                await _Context.SaveChangesAsync();  // commit the transction
                return lab.TestId;
            }
            return 0;
        }
        #endregion

        #region Update Lab
        public async Task UpdateLabTest(Laboratory lab)
        {
            if (_Context != null)
            {
                _Context.Entry(lab).State = EntityState.Modified;
                _Context.Laboratory.Update(lab);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion

        #region GetLabById
        public async Task<Laboratory> GetLabById(int? id)
        {
            if (_Context != null)
            {
                var employee = await _Context.Laboratory.FindAsync(id);   //primary key
                return employee;
            }
            return null;
        }
        #endregion

        #region Delete Lab
        public async Task<int> DeleteLabTest(int? id)
        {
            if (_Context != null)
            {
                var lab = await (_Context.Laboratory.FirstOrDefaultAsync(emp => emp.TestId == id));

                if (lab != null)
                {
                    //Delete
                    _Context.Laboratory.Remove(lab);

                    //Commit
                    await _Context.SaveChangesAsync();
                    return lab.TestId;
                }
            }
            return 0;
        }
        #endregion
    }
}
