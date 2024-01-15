using FinalCMS.Models;
using FinalCMS.Receptionist_ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
        public async Task<Appointment_ViewModel> BookAppointment(Appointment_ViewModel viewModel,bool isNewPatient) 
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
                decimal registerFee;
                if (isNewPatient)
                {
                    registerFee = viewModel.RegisterFees ?? 150;

                }
                else
                {
                    registerFee = viewModel.RegisterFees ?? 0;
                }

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
        #region Display Bill Details
        public async Task<BillViewModel> BillDetails(int? billId)
        {
            if (_context == null)
                return null;

            var bill = await _context.ConsultBill.FindAsync(billId);
            if (bill == null)
                return null;

            var appointment = await GetAppointmentDetails(bill.AppointmentId);
            if (appointment == null)
                return null;

            var patient = await GetPatientDetails(appointment.PatientId);
            var doctor = await GetDoctorDetails(appointment.DoctorId);
            if (doctor == null)
                return null;

            var specialization = await GetSpecializationDetails(doctor.SpecializationId);
            var department = await GetDepartmentDetails(specialization.DepartmentId);
            var staff = await GetStaffDetails(doctor.StaffId);

            if (staff == null || specialization == null || department == null)
                return null;

            return CreateBillViewModel(patient, doctor, staff, specialization, department, appointment, bill);
        }

        private async Task<Appointment> GetAppointmentDetails(int? appointmentId)
        {
            return await _context.Appointment.FindAsync(appointmentId);
        }

        private async Task<Patient> GetPatientDetails(int? patientId)
        {
            return await _context.Patient.FindAsync(patientId);
        }

        private async Task<Doctor> GetDoctorDetails(int? doctorId)
        {
            return await _context.Doctor.FindAsync(doctorId);
        }

        private async Task<Specialization> GetSpecializationDetails(int? specializationId)
        {
            return await _context.Specialization.FindAsync(specializationId);
        }

        private async Task<Department> GetDepartmentDetails(int? departmentId)
        {
            return await _context.Department.FindAsync(departmentId);
        }

        private async Task<Staff> GetStaffDetails(int? staffId)
        {
            return await _context.Staff.FindAsync(staffId);
        }

        private BillViewModel CreateBillViewModel(Patient patient, Doctor doctor, Staff staff, Specialization specialization, Department department, Appointment appointment, ConsultBill bill)
        {
            return new BillViewModel()
            {
                PatientId = patient.PatientId,
                PatientName = patient.PatientName,
                RegisterNo = patient.RegisterNo,
                DoctorId = doctor.DoctorId,
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                RegisterFees = bill.RegisterFees,
                ConsultationFee = doctor.ConsultationFee,
                SpecializationId = specialization.SpecializationId,
                TotalAmt = bill.TotalAmt,
                DepartmentId = department.DepartmentId,
                Department1 = department.Department1,
                AppointmentId = appointment.AppointmentId,
                TokenNo = appointment.TokenNo,
                AppointmentDate = appointment.AppointmentDate,
                CheckUpStatus = appointment.CheckUpStatus,
                BillId = bill.BillId,
            };
        }

        #endregion

        #region Get All Appointments with BillViewModel
        public async Task<List<BillViewModel>> GetAllAppointmentsWithBillViewModel()
        {
            if (_context != null)
            {
                var appointments = await _context.Appointment
             .Include(a => a.Patient)
             .Include(a => a.Doctor).ThenInclude
             (d=>d.Staff).Include(a=>a.Doctor).ThenInclude(d=>d.Specialization).ThenInclude(s=>s.Department)
             .ToListAsync();
                // Enable logging to console
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                        .AddConsole();
                });

                _context.Database.SetCommandTimeout(180);
                _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

                // Your actual query to retrieve Staff with Department
                var staffWithDepartment = await _context.Staff
                    .Include(s => s.Department)
                    .FirstOrDefaultAsync(s=>s.DepartmentId==s.Department.DepartmentId);

                // Disable logging after the query (optional)
                _context.Database.SetCommandTimeout(30); // Set the timeout to its original value
                _context.GetService<ILoggerFactory>().Dispose();

                var appointmentsWithViewModel = new List<BillViewModel>();

                foreach (var appointment in appointments)
                {
                    // Find the ConsultBill for the current appointment
                    var consultBill = await _context.ConsultBill
                        .FirstOrDefaultAsync(b => b.AppointmentId == appointment.AppointmentId);

                    // Transform Appointment entity and ConsultBill to BillViewModel
                    var appointmentWithViewModel = new BillViewModel
                    {
                        AppointmentId = appointment.AppointmentId,
                        TokenNo = appointment.TokenNo,
                        AppointmentDate = appointment.AppointmentDate,
                        PatientId = appointment.PatientId,
                        DoctorId = appointment.DoctorId,
                        CheckUpStatus = appointment.CheckUpStatus,
                        BillId = consultBill.BillId,
                        RegisterFees = consultBill?.RegisterFees,
                        TotalAmt = consultBill.TotalAmt,
                        ConsultationFee = appointment.Doctor?.ConsultationFee,
                        StaffId = appointment.Doctor?.StaffId,
                        SpecializationId = appointment.Doctor?.SpecializationId,
                        DepartmentId = appointment.Doctor?.Specialization?.DepartmentId,
                        Department1 = appointment.Doctor?.Specialization?.Department?.Department1,
                        StaffName = appointment.Doctor?.Staff?.StaffName,
                        RegisterNo = appointment.Patient?.RegisterNo,
                        PatientName = appointment.Patient?.PatientName
                    };

                    appointmentsWithViewModel.Add(appointmentWithViewModel);
                }


                return appointmentsWithViewModel;
            }
            return null;
        }
        #endregion

    }
}
