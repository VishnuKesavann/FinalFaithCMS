using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class LabReportGeneration
    {
        public LabReportGeneration()
        {
            LabBillGeneration = new HashSet<LabBillGeneration>();
        }

        public int LabreportId { get; set; }
        public string LabResult { get; set; }
        public DateTime? ReportDate { get; set; }
        public string Remarks { get; set; }
        public int? LabPrescId { get; set; }
        public int? AppointmentId { get; set; }
        public int? TestId { get; set; }
        public int? StaffId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual LabPrescriptions LabPresc { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Laboratory Test { get; set; }
        public virtual ICollection<LabBillGeneration> LabBillGeneration { get; set; }
    }
}
