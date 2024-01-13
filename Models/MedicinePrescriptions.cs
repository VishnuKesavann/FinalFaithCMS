using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class MedicinePrescriptions
    {
        public int MedPrescriptionId { get; set; }
        public int? PrescribedMedicineId { get; set; }
        public string Dosage { get; set; }
        public int? DosageDays { get; set; }
        public int? MedicineQuantity { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Medicine PrescribedMedicine { get; set; }
    }
}
