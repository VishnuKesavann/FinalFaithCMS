using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
            LabBillGeneration = new HashSet<LabBillGeneration>();
        }

        public int PatientId { get; set; }
        public string RegisterNo { get; set; }
        public string PatientName { get; set; }
        public DateTime PatientDob { get; set; }
        public string PatientAddr { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public long PhoneNumber { get; set; }
        public string PatientEmail { get; set; }
        public string PatientStatus { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<LabBillGeneration> LabBillGeneration { get; set; }
    }
}
