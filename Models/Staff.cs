using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Doctor = new HashSet<Doctor>();
            LabReportGeneration = new HashSet<LabReportGeneration>();
        }

        public int StaffId { get; set; }
        public int? QualificationId { get; set; }
        public int? LoginId { get; set; }
        public string StaffName { get; set; }
        public DateTime? StaffDob { get; set; }
        public string StaffGender { get; set; }
        public string StaffAddress { get; set; }
        public string StaffBloodgroup { get; set; }
        public DateTime? StaffJoindate { get; set; }
        public int? StaffSalary { get; set; }
        public long? StaffMobieno { get; set; }
        public int? DepartmentId { get; set; }
        public string EMail { get; set; }
        public int? SpecializationId { get; set; }
        public int? RoleId { get; set; }

        public virtual Department Department { get; set; }
        public virtual UserLogin Login { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Role Role { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<LabReportGeneration> LabReportGeneration { get; set; }
    }
}
