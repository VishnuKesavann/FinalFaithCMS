using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            ConsultBill = new HashSet<ConsultBill>();
            LabBillGeneration = new HashSet<LabBillGeneration>();
            LabReportGeneration = new HashSet<LabReportGeneration>();
        }

        public int AppointmentId { get; set; }
        public int TokenNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string CheckUpStatus { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<ConsultBill> ConsultBill { get; set; }
        public virtual ICollection<LabBillGeneration> LabBillGeneration { get; set; }
        public virtual ICollection<LabReportGeneration> LabReportGeneration { get; set; }
    }
}
