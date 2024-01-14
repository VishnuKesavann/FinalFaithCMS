using FinalCMS.AdminViewModel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FinalCMS.AdminRepository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly FinalCMS_dbContext _context;

        public StaffRepository(FinalCMS_dbContext context)
        {
            _context = context;
        }

        #region List the staff
        public async Task<List<StaffViewModel>> GetViewModelEmployees()
        {
            if (_context != null)
            {
                var staffDetails = await (from staff in _context.Staff
                                          join role in _context.Role on staff.RoleId equals role.RoleId
                                          join specialization in _context.Specialization on staff.SpecializationId equals specialization.SpecializationId into specJoin
                                          from spec in specJoin.DefaultIfEmpty()
                                          join department in _context.Department on staff.DepartmentId equals department.DepartmentId into deptJoin
                                          from dept in deptJoin.DefaultIfEmpty()
                                          where role.RoleName.ToLower() != "doctor" || (role.RoleName.ToLower() == "doctor" && spec != null && dept != null)
                                          select new StaffViewModel
                                          {
                                              StaffName = staff.StaffName,
                                              StaffDob = staff.StaffDob,
                                              StaffGender = staff.StaffGender,
                                              StaffAddress = staff.StaffAddress,
                                              StaffBloodgroup = staff.StaffBloodgroup,
                                              StaffJoindate = staff.StaffJoindate,
                                              StaffSalary = staff.StaffSalary,
                                              StaffMobieno = staff.StaffMobieno,
                                              EMail = staff.EMail,
                                              Qualification1 = staff.Qualification.Qualification1,
                                              RoleName=staff.Role.RoleName,
                                              Department1 = dept.Department1,
                                              SpecializationName = spec.SpecializationName,
                                              ConsultationFee = staff.Doctor.FirstOrDefault().ConsultationFee,
                                              UserName = staff.Login.UserName,
                                              Password = staff.Login.Password
                                          }).ToListAsync();

                return staffDetails;


            }

            return null;
        }
        #endregion

        #region add new staff
        public async Task<int> AddStaff(Staff staff)
        {
            if (_context != null)
            {
                // Add the new staff to the database
                _context.Staff.Add(staff);
                await _context.SaveChangesAsync();

                // Return the newly added staff's ID
                int newStaffId = staff.StaffId;

                // If the role is Doctor, update additional details
                if (staff.Role?.RoleName.ToLower() == "doctor") // Adjusted condition to check RoleName
                {
                    // Assuming Doctor details are provided in the staff object, adjust this based on your actual structure
                    Doctor doctorDetails = staff.Doctor.FirstOrDefault();

                    if (doctorDetails != null)
                    {
                        doctorDetails.StaffId = newStaffId;
                        _context.Doctor.Add(doctorDetails);
                        await _context.SaveChangesAsync();
                    }
                }

                return newStaffId;
            }
            return 0;
        }
        #endregion
        #region update staff
        public async Task UpdateStaff(Staff staff)
        {
            if (_context != null)
            {
                // Update the staff in the database
                _context.Entry(staff).State = EntityState.Modified;
                _context.Staff.Update(staff);
                await _context.SaveChangesAsync();

                // If the role is Doctor, update additional details
                if (staff.Role?.RoleName.ToLower() == "doctor") // Adjusted condition to check RoleName
                {
                    // Assuming Doctor details are provided in the staff object, adjust this based on your actual structure
                    Doctor doctorDetails = staff.Doctor.FirstOrDefault();

                    if (doctorDetails != null)
                    {
                        // Assuming Doctor details have a reference to the Staff
                        doctorDetails.StaffId = staff.StaffId;
                        _context.Entry(doctorDetails).State = EntityState.Modified;
                        _context.Doctor.Update(doctorDetails);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        #endregio+

    }

}

