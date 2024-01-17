using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalCMS.Models
{
    public partial class FinalCMS_dbContext : DbContext
    {
        public FinalCMS_dbContext()
        {
        }

        public FinalCMS_dbContext(DbContextOptions<FinalCMS_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<ConsultBill> ConsultBill { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<LabBillGeneration> LabBillGeneration { get; set; }
        public virtual DbSet<LabPrescriptions> LabPrescriptions { get; set; }
        public virtual DbSet<LabReportGeneration> LabReportGeneration { get; set; }
        public virtual DbSet<Laboratory> Laboratory { get; set; }
        public virtual DbSet<LoginDetails> LoginDetails { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<MedicinePrescriptions> MedicinePrescriptions { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientHistory> PatientHistory { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

       /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = VISHNU\\SQLEXPRESS; Initial Catalog = FinalCMS_db; Integrated security = True");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("appointment_Date")
                    .HasColumnType("date");

                entity.Property(e => e.CheckUpStatus)
                    .IsRequired()
                    .HasColumnName("CheckUp_Status")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CONFIRMED')");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_ID");

                entity.Property(e => e.PatientId).HasColumnName("patient_Id");

                entity.Property(e => e.TokenNo).HasColumnName("token_No");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient1");
            });

            modelBuilder.Entity<ConsultBill>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__Consult___D733892B1068F748");

                entity.ToTable("Consult_Bill");

                entity.Property(e => e.BillId).HasColumnName("bill_Id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.RegisterFees)
                    .HasColumnName("register_Fees")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalAmt)
                    .HasColumnName("total_Amt")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.ConsultBill)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment1");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasColumnName("department_Id");

                entity.Property(e => e.Department1)
                    .HasColumnName("department")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.Property(e => e.DiagnosisId).HasColumnName("diagnosisId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.Diagnosis1)
                    .HasColumnName("diagnosis")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Symptoms)
                    .IsRequired()
                    .HasColumnName("symptoms")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Diagnosis)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Diagnosis__appoi__4E88ABD4");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctor_ID");

                entity.Property(e => e.ConsultationFee).HasColumnName("consultation_Fee");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_Id");

                entity.Property(e => e.StaffId).HasColumnName("staff_Id");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__Doctor__speciali__34C8D9D1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Doctor__staff_Id__35BCFE0A");
            });

            modelBuilder.Entity<LabBillGeneration>(entity =>
            {
                entity.HasKey(e => e.LabbillId)
                    .HasName("PK__LabBillG__E68C5DADE0031676");

                entity.Property(e => e.LabbillId).HasColumnName("labbill_Id");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.C)
                    .HasColumnName("c")
                    .HasColumnType("date");

                entity.Property(e => e.LabreportId).HasColumnName("labreport_Id");

                entity.Property(e => e.PatientId).HasColumnName("Patient_Id");

                entity.Property(e => e.TestId).HasColumnName("test_Id");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_Amount")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.LabBillGeneration)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__LabBillGe__appoi__5812160E");

                entity.HasOne(d => d.Labreport)
                    .WithMany(p => p.LabBillGeneration)
                    .HasForeignKey(d => d.LabreportId)
                    .HasConstraintName("FK__LabBillGe__labre__59063A47");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.LabBillGeneration)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__LabBillGe__Patie__59FA5E80");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.LabBillGeneration)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__LabBillGe__test___5AEE82B9");
            });

            modelBuilder.Entity<LabPrescriptions>(entity =>
            {
                entity.HasKey(e => e.LabPrescriptionId)
                    .HasName("PK__LabPresc__22C099BAC3C3B3C4");

                entity.Property(e => e.LabPrescriptionId).HasColumnName("labPrescriptionId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.LabNote)
                    .HasColumnName("labNote")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LabTestId).HasColumnName("labTestId");

                entity.Property(e => e.LabTestStatus)
                    .IsRequired()
                    .HasColumnName("labTestStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.LabPrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__LabPrescr__appoi__4F7CD00D");

                entity.HasOne(d => d.LabTest)
                    .WithMany(p => p.LabPrescriptions)
                    .HasForeignKey(d => d.LabTestId)
                    .HasConstraintName("FK__LabPrescr__labTe__5165187F");
            });

            modelBuilder.Entity<LabReportGeneration>(entity =>
            {
                entity.HasKey(e => e.LabreportId)
                    .HasName("PK__LabRepor__775EB8617127A401");

                entity.Property(e => e.LabreportId).HasColumnName("labreport_Id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_Id");

                entity.Property(e => e.LabPrescId).HasColumnName("labPresc_Id");

                entity.Property(e => e.LabResult)
                    .IsRequired()
                    .HasColumnName("lab_Result")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_Date")
                    .HasColumnType("date");

                entity.Property(e => e.StaffId).HasColumnName("staff_Id");

                entity.Property(e => e.TestId).HasColumnName("test_Id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.LabReportGeneration)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__LabReport__appoi__5BE2A6F2");

                entity.HasOne(d => d.LabPresc)
                    .WithMany(p => p.LabReportGeneration)
                    .HasForeignKey(d => d.LabPrescId)
                    .HasConstraintName("FK__LabReport__labPr__5CD6CB2B");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.LabReportGeneration)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__LabReport__staff__5DCAEF64");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.LabReportGeneration)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__LabReport__test___5EBF139D");
            });

            modelBuilder.Entity<Laboratory>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__Laborato__F3FE002A66DE3307");

                entity.Property(e => e.TestId).HasColumnName("test_Id");

                entity.Property(e => e.HighRange)
                    .HasColumnName("high_Range")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LowRange)
                    .HasColumnName("low_Range")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TestName)
                    .HasColumnName("test_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TestPrice)
                    .HasColumnName("test_Price")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<LoginDetails>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.Property(e => e.MedicineId).HasColumnName("medicine_Id");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenericName)
                    .HasColumnName("generic_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineCode).HasColumnName("medicine_Code");

                entity.Property(e => e.MedicineName)
                    .HasColumnName("medicine_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineStock).HasColumnName("medicine_Stock");

                entity.Property(e => e.MedicineUnitPrice)
                    .HasColumnName("medicine_unitPrice")
                    .HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<MedicinePrescriptions>(entity =>
            {
                entity.HasKey(e => e.MedPrescriptionId);

                entity.Property(e => e.MedPrescriptionId).HasColumnName("medPrescriptionId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointmentId");

                entity.Property(e => e.Dosage)
                    .HasColumnName("dosage")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DosageDays).HasColumnName("dosageDays");

                entity.Property(e => e.MedicineQuantity).HasColumnName("medicineQuantity");

                entity.Property(e => e.PrescribedMedicineId).HasColumnName("prescribedMedicineId");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.MedicinePrescriptions)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__MedicineP__appoi__5070F446");

                entity.HasOne(d => d.PrescribedMedicine)
                    .WithMany(p => p.MedicinePrescriptions)
                    .HasForeignKey(d => d.PrescribedMedicineId)
                    .HasConstraintName("FK__MedicineP__presc__52593CB8");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patient_Id");

                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasColumnName("bloodGroup")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PatientAddr)
                    .IsRequired()
                    .HasColumnName("patient_Addr")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PatientDob)
                    .HasColumnName("patient_DOB")
                    .HasColumnType("date");

                entity.Property(e => e.PatientEmail)
                    .HasColumnName("patient_Email")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PatientName)
                    .IsRequired()
                    .HasColumnName("patient_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PatientStatus)
                    .IsRequired()
                    .HasColumnName("patient_status")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ACTIVE')");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.RegisterNo)
                    .HasColumnName("register_No")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('P'+CONVERT([varchar](15),[patient_Id]))");
            });

            modelBuilder.Entity<PatientHistory>(entity =>
            {
                entity.Property(e => e.PatientHistoryId).HasColumnName("patientHistoryId");

                entity.Property(e => e.DiagnosisId).HasColumnName("diagnosisId");

                entity.Property(e => e.LabPrescriptionId).HasColumnName("labPrescriptionId");

                entity.Property(e => e.LabReportId).HasColumnName("labReportId");

                entity.Property(e => e.MedPrescriptionId).HasColumnName("medPrescriptionId");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.PatientHistory)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK__PatientHi__diagn__534D60F1");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.QualificationId).HasColumnName("qualification_Id");

                entity.Property(e => e.Qualification1)
                    .HasColumnName("qualification")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("role_Id");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.Property(e => e.SpecializationId).HasColumnName("specialization_Id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_Id");

                entity.Property(e => e.SpecializationName)
                    .HasColumnName("specialization_Name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Specialization)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Specializ__depar__36B12243");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnName("staff_Id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_Id");

                entity.Property(e => e.EMail)
                    .HasColumnName("E_mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId).HasColumnName("login_Id");

                entity.Property(e => e.QualificationId).HasColumnName("qualification_Id");

                entity.Property(e => e.RoleId).HasColumnName("role_Id");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_Id");

                entity.Property(e => e.StaffAddress)
                    .HasColumnName("staff_Address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffBloodgroup)
                    .HasColumnName("staff_Bloodgroup")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffDob)
                    .HasColumnName("staff_Dob")
                    .HasColumnType("date");

                entity.Property(e => e.StaffGender)
                    .HasColumnName("staff_Gender")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffJoindate)
                    .HasColumnName("staff_Joindate")
                    .HasColumnType("date");

                entity.Property(e => e.StaffMobieno).HasColumnName("staff_Mobieno");

                entity.Property(e => e.StaffName)
                    .HasColumnName("staff_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffSalary).HasColumnName("staff_Salary");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__Staff__departmen__37A5467C");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK__Staff__login_Id__38996AB5");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK__Staff__qualifica__398D8EEE");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Staff__role_Id__3A81B327");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__Staff__specializ__3B75D760");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__UserLogi__C2CA7DB37E0E4660");

                entity.Property(e => e.LoginId).HasColumnName("login_ID");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("role_Id");

                entity.Property(e => e.UserName)
                    .HasColumnName("user_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserLogin)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserLogin__role___3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
