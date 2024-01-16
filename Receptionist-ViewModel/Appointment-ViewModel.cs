using System;

namespace FinalCMS.Receptionist_ViewModel
{
    public class Appointment_ViewModel
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
       
    }
}
