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
    }
}
