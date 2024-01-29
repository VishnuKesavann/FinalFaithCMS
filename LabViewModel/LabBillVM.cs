using System;

namespace FinalCMS.LabViewModel
{
    public class LabBillVM
    {
        public int LabbillId { get; set; }
        public DateTime? C { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Amount { get; set; }
        public int? PatientId { get; set; }
        public int? TestId { get; set; }
        public string? TestName { get; set; }
        public int? LabreportId { get; set; }
        public int? AppointmentId { get; set; }
        public string? PatientName { get; set; }
    }
}
