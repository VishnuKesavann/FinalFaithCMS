using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DoctorId { get; set; }
        public int? ConsultationFee { get; set; }
        public int? StaffId { get; set; }
        public int? SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
