using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class LabBillGeneration
    {
        public int LabbillId { get; set; }
        public DateTime? C { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Amount { get; set; }
        public int? PatientId { get; set; }
        public int? TestId { get; set; }
        public int? LabreportId { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual LabReportGeneration Labreport { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Laboratory Test { get; set; }
    }
}
