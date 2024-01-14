using System;

namespace FinalCMS.Receptionist_ViewModel
{
    public class BillViewModel
    {
        public int AppointmentId { get; set; }
        public int TokenNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string CheckUpStatus { get; set; }
        public int BillId { get; set; }
        public decimal? RegisterFees { get; set; }
        public decimal TotalAmt { get; set; }
        public int? ConsultationFee { get; set; }
        public int? StaffId { get; set; }
        public int? SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        public string Department1 { get; set; }
        
        public string StaffName { get; set; }
        public string RegisterNo { get; set; }
        public string PatientName { get; set; }

    }
}
