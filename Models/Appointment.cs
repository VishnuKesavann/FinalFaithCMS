using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            ConsultBill = new HashSet<ConsultBill>();
            Diagnosis = new HashSet<Diagnosis>();
            LabBillGeneration = new HashSet<LabBillGeneration>();
            LabPrescriptions = new HashSet<LabPrescriptions>();
            LabReportGeneration = new HashSet<LabReportGeneration>();
            MedicinePrescriptions = new HashSet<MedicinePrescriptions>();
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
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        public virtual ICollection<LabBillGeneration> LabBillGeneration { get; set; }
        public virtual ICollection<LabPrescriptions> LabPrescriptions { get; set; }
        public virtual ICollection<LabReportGeneration> LabReportGeneration { get; set; }
        public virtual ICollection<MedicinePrescriptions> MedicinePrescriptions { get; set; }
    }
}
