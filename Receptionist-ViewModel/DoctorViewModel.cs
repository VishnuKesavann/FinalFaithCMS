using System;

namespace FinalCMS.Receptionist_ViewModel
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public int? ConsultationFee { get; set; }
        public int? StaffId { get; set; }
        public int? SpecializationId { get; set; }
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
        public int? RoleId { get; set; }

    }
}
