using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class PatientHistory
    {
        public int PatientHistoryId { get; set; }
        public int? LabReportId { get; set; }
        public int? DiagnosisId { get; set; }
        public int? MedPrescriptionId { get; set; }
        public int? LabPrescriptionId { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
    }
}
