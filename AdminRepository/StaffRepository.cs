using FinalCMS.AdminViewModel;
using FinalCMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FinalCMS.AdminRepository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly FinalCMS_dbContext _Context;

        public StaffRepository(FinalCMS_dbContext context)
        {
            _Context = context;
        }
        #region list staff
        public async Task<List<StaffViewModel>> GetStaffDetails()
        {

            if (_Context != null)
            {
                var staffDetails = await(from staff in _Context.Staff
                                         join department in _Context.Department on staff.DepartmentId equals department.DepartmentId into deptGroup
                                         from dept in deptGroup.DefaultIfEmpty()
                                         join specialization in _Context.Specialization on dept.DepartmentId equals specialization.DepartmentId into specGroup
                                         from spec in specGroup.DefaultIfEmpty()
                                         join qualification in _Context.Qualification on staff.QualificationId equals qualification.QualificationId into qualGroup
                                         from qual in qualGroup.DefaultIfEmpty()
                                         join role in _Context.Role on staff.RoleId equals role.RoleId into roleGroup
                                         from role in roleGroup.DefaultIfEmpty()
                                         join doctor in _Context.Doctor on staff.StaffId equals doctor.StaffId into doctorGroup
                                         from doctor in doctorGroup.DefaultIfEmpty()
                                         join login in _Context.UserLogin on staff.LoginId equals login.LoginId into loginGroup
                                         from login in loginGroup.DefaultIfEmpty()

                                         select new StaffViewModel
                                         {
                                             StaffId = staff.StaffId,
                                             StaffName = staff.StaffName,
                                             StaffDob = staff.StaffDob,
                                             StaffGender = staff.StaffGender,
                                             StaffAddress = staff.StaffAddress,
                                             StaffBloodgroup = staff.StaffBloodgroup,
                                             StaffJoindate = staff.StaffJoindate,
                                             StaffSalary = staff.StaffSalary,
                                             StaffMobieno = staff.StaffMobieno,
                                             Department1 = dept != null ? dept.Department1 : null,
                                             Qualification1 = qual != null ? qual.Qualification1 : null,
                                             SpecializationName = spec != null ? spec.SpecializationName : null,
                                             RoleName = role != null ? role.RoleName : null,
                                             ConsultationFee = (int?)(doctor != null ? doctor.ConsultationFee : (decimal?)null),
                                             UserName = login.UserName,
                                             Password = login.Password,
                                             SpecializationId = (int)(spec != null ? spec.SpecializationId : (int?)null),
                                             RoleId = (int)(role != null ? role.RoleId : (int?)null),
                                             LoginId = login.LoginId,
                                             DepartmentId = (int)(dept != null ? dept.DepartmentId : (int?)null),
                                             QualificationId = (int)(qual != null ? qual.QualificationId : (int?)null)
                                         }).ToListAsync();

                return staffDetails;
            }

            // If _Context is null, return an empty list or handle it according to your requirements.
            return null;
        }
        #endregion
        #region get by id
        public async Task<StaffViewModel> GetStaffDetailsById(int? staffId)
        {
            if (_Context != null)
            {
                var staffDetails = await(from staff in _Context.Staff
                                         where staff.StaffId == staffId
                                         join department in _Context.Department on staff.DepartmentId equals department.DepartmentId into deptGroup
                                         from dept in deptGroup.DefaultIfEmpty()
                                         join specialization in _Context.Specialization on dept.DepartmentId equals specialization.DepartmentId into specGroup
                                         from spec in specGroup.DefaultIfEmpty()
                                         join qualification in _Context.Qualification on staff.QualificationId equals qualification.QualificationId into qualGroup
                                         from qual in qualGroup.DefaultIfEmpty()
                                         join role in _Context.Role on staff.RoleId equals role.RoleId into roleGroup
                                         from role in roleGroup.DefaultIfEmpty()
                                         join doctor in _Context.Doctor on staff.StaffId equals doctor.StaffId into doctorGroup
                                         from doctor in doctorGroup.DefaultIfEmpty()
                                         join login in _Context.UserLogin on staff.LoginId equals login.LoginId into loginGroup
                                         from login in loginGroup.DefaultIfEmpty()
                                         select new StaffViewModel
                                         {
                                             StaffId = staff.StaffId,
                                             StaffName = staff.StaffName,
                                             StaffDob = staff.StaffDob,
                                             StaffGender = staff.StaffGender,
                                             StaffAddress = staff.StaffAddress,
                                             StaffBloodgroup = staff.StaffBloodgroup,
                                             StaffJoindate = staff.StaffJoindate,
                                             StaffSalary = staff.StaffSalary,
                                             StaffMobieno = staff.StaffMobieno,
                                             Department1 = dept != null ? dept.Department1 : null,
                                             Qualification1 = qual != null ? qual.Qualification1 : null,
                                             SpecializationName = spec != null ? spec.SpecializationName : null,
                                             RoleName = role != null ? role.RoleName : null,
                                             ConsultationFee = (int?)(doctor != null ? doctor.ConsultationFee : (decimal?)null),
                                             UserName = login.UserName,
                                             Password = login.Password
                                         }).FirstOrDefaultAsync();

                return staffDetails;
            }

            // If _Context is null, return null or handle it according to your requirements.
            return null;
        }
        #endregion


        #region Update staff
        public async Task UpdateStaff(Staff staff)
        {
            if (_Context != null)
            {
                _Context.Entry(staff).State = EntityState.Modified;
                _Context.Staff.Update(staff);
                await _Context.SaveChangesAsync();
            }
        }
        #endregion


        public async Task<int> AddStaffWithRelatedData(StaffViewModel staffDetails)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();




            try
            {
                var nelogin = new UserLogin
                {
                    UserName = staffDetails.UserName,
                    Password = staffDetails.Password,
                    RoleId = staffDetails.RoleId
                };
                _Context.UserLogin.Add(nelogin);
                await _Context.SaveChangesAsync();
                // Add staff to the staff table
                var newStaff = new Staff
                {
                    StaffName = staffDetails.StaffName,
                    StaffDob = staffDetails.StaffDob,
                    StaffGender = staffDetails.StaffGender,
                    StaffAddress = staffDetails.StaffAddress,
                    StaffBloodgroup = staffDetails.StaffBloodgroup,
                    StaffJoindate = staffDetails.StaffJoindate,
                    StaffSalary = staffDetails.StaffSalary,
                    StaffMobieno = staffDetails.StaffMobieno,
                    EMail = staffDetails.EMail,
                    RoleId = staffDetails.RoleId,
                    SpecializationId = staffDetails.SpecializationId,
                    DepartmentId = staffDetails.DepartmentId,
                    QualificationId = staffDetails.QualificationId,
                    LoginId = nelogin.LoginId


                };


                // Add staff to the staff table
                _Context.Staff.Add(newStaff);
                await _Context.SaveChangesAsync();

                // Retrieve the automatically generated StaffId
                int newStaffId = newStaff.StaffId;




                if (staffDetails.RoleId == 3) // Assuming RoleId 3 corresponds to the doctor role
                {
                    // Retrieve DepartmentId for the given DepartmentName

                    // Add to TblDoctors for doctor role
                    var doctor = new Doctor
                    {
                        StaffId = newStaffId,
                        ConsultationFee = staffDetails.ConsultationFee,
                        SpecializationId = staffDetails.SpecializationId
                    };

                    _Context.Doctor.Add(doctor);
                    await _Context.SaveChangesAsync();
                }






                await transaction.CommitAsync();

                // Return the ID of the newly added staff
                return newStaffId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception or handle it as needed
                throw;
            }



        }
    }
}

