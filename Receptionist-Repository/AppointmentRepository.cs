using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCMS.Receptionist_Repository
{
    public class AppointmentRepository:IAppointmentRepository
    {
        private readonly FinalCMS_dbContext _context;
        public AppointmentRepository(FinalCMS_dbContext context)
        {
                _context = context;
        }
        #region Get All Departments
        public async Task<List<Department>> GetAllDepartment()
        {
            if (_context!=null)
            {
                return await _context.Department.ToListAsync();

            }
            return null;
        }
        #endregion

        #region Get all Specialization By Department Id
        public async Task<List<Specialization>> GetAllSpecializationByDepartmentId(int? departmentId) 
        {
            if (_context!=null)
            {
                return await _context.Specialization.Where(s => s.DepartmentId == departmentId).ToListAsync();
            }
            return null;
        }
        #endregion

    }
}
