using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class LabPrescriptions
    {
        public LabPrescriptions()
        {
            LabReportGeneration = new HashSet<LabReportGeneration>();
        }

        public int LabPrescriptionId { get; set; }
        public int? LabTestId { get; set; }
        public string LabNote { get; set; }
        public string LabTestStatus { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Laboratory LabTest { get; set; }
        public virtual ICollection<LabReportGeneration> LabReportGeneration { get; set; }
    }
}
