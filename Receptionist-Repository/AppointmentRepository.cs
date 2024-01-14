using FinalCMS.Models;
using FinalCMS.Receptionist_ViewModel;
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
        #region Get Doctor by Specializaton Id
        public async Task<List<DoctorViewModel>> GetAllDoctorBySpecializationId(int? specializationId)
        {
            if (_context != null)
            {
                var doctors = await _context.Doctor
            .Where(d => d.SpecializationId == specializationId)
            .Include(d => d.Staff)
            .Include(d => d.Staff.Specialization)
            .Select(d => new DoctorViewModel
            {
                DoctorId = d.DoctorId,
                ConsultationFee = d.ConsultationFee,
                StaffId = d.StaffId,
                SpecializationId = d.SpecializationId,
                QualificationId = d.Staff.QualificationId,
                LoginId = d.Staff.LoginId,
                StaffName = d.Staff.StaffName,
                StaffDob = d.Staff.StaffDob,
                StaffGender = d.Staff.StaffGender,
                StaffAddress = d.Staff.StaffAddress,
                StaffBloodgroup = d.Staff.StaffBloodgroup,
                StaffJoindate = d.Staff.StaffJoindate,
                StaffSalary = d.Staff.StaffSalary,
                StaffMobieno = d.Staff.StaffMobieno,
                DepartmentId = d.Staff.DepartmentId,
                EMail = d.Staff.EMail,
                RoleId = d.Staff.RoleId
            })
            .ToListAsync();

                return doctors;

            }
            return null;
        }
        #endregion
    }
}
