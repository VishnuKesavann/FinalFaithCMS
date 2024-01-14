using FinalCMS.Models;
using FinalCMS.Receptionist_ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
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
        #region Book Appointment and Bill Genereation
        public async Task<Appointment_ViewModel> BookAppointment(Appointment_ViewModel viewModel) 
        {   
            var existingAppointment = await _context.Appointment.Where(a => a.PatientId == viewModel.PatientId
            && a.DoctorId == viewModel.DoctorId && a.AppointmentDate == viewModel.AppointmentDate).FirstOrDefaultAsync();
            if (existingAppointment!=null)
            {
                throw new InvalidOperationException("Appointment already exists for the same doctor and same date");
            }
            if (_context!=null)
            {
                var lastTokenNumber = await _context.Appointment.Where(a => a.DoctorId == viewModel.DoctorId && a.AppointmentDate == viewModel.AppointmentDate).OrderByDescending(a => a.TokenNo).Select(a => a.TokenNo).FirstOrDefaultAsync();

                int newTokenNumber;
                if (lastTokenNumber >-1 && lastTokenNumber < 100)
                {
                    newTokenNumber = lastTokenNumber + 1;
                }
                else
                {
                    throw new InvalidOperationException("No more token is available for today");
                }
                viewModel.TokenNo = newTokenNumber;
                var newAppointment = new Appointment()
                {
                    TokenNo = viewModel.TokenNo,
                    PatientId = viewModel.PatientId,
                    AppointmentDate = viewModel.AppointmentDate,
                    DoctorId = viewModel.DoctorId,
                    
                };
                _context.Appointment.Add(newAppointment);
                await _context.SaveChangesAsync();

                viewModel.AppointmentId = newAppointment.AppointmentId;
                viewModel.CheckUpStatus= viewModel.CheckUpStatus;
                decimal registerFee = viewModel.RegisterFees ?? 150;
                decimal consultFees = viewModel.ConsultationFee ?? _context.Doctor.Where(d => d.DoctorId == viewModel.DoctorId).Select(d => (decimal?)d.ConsultationFee).FirstOrDefault() ?? 0;
                decimal totalAmount = registerFee + consultFees + (0.18m * registerFee) + (0.18m * consultFees);

                viewModel.RegisterFees = registerFee;
                viewModel.ConsultationFee =(int ?) consultFees;
                viewModel.TotalAmt = totalAmount;
                var newConsultBill = new ConsultBill()
                {
                    AppointmentId = viewModel.AppointmentId,
                    RegisterFees = registerFee,
                    TotalAmt = totalAmount
                };


                _context.ConsultBill.Add(newConsultBill);
                await _context.SaveChangesAsync();
                viewModel.BillId = newConsultBill.BillId;
                return viewModel;
            }
            return null;
        }
        #endregion
    }
}
