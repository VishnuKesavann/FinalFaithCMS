using System;

namespace FinalCMS.LabViewModel
{
    public class LabReportVM
    {
        public int ReportId { get; set; }

        public DateTime? ReportDate { get; set; }

        public string PatientName { get; set; }

        public string TestName { get; set; }

        public string LowRange { get; set; }

        public string HighRange { get; set; }

        public string TestResult { get; set; }

        public string Remarks { get; set; }
        public int? LabPrescId { get; set; }
        public int? AppointmentId { get; set; }
        public int? TestId { get; set; }
        public int? StaffId { get; set; }
        public int LabreportId { get; set; }
        public string LabResult { get; set; }
       

    }
}
