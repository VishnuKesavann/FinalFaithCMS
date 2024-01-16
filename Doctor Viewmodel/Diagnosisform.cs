namespace FinalCMS.Doctor_Viewmodel
{
    public class Diagnosisform
    {

        public int DiagnosisId { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis1 { get; set; }
        public string Note { get; set; }
        public int? AppointmentId { get; set; }
        public int LabPrescriptionId { get; set; }
        public int? LabTestId { get; set; }
        public string LabNote { get; set; }
        public string LabTestStatus { get; set; }
        public int MedPrescriptionId { get; set; }
        public int PrescribedMedicineId { get; set; }
        public string Dosage { get; set; }
        public int? DosageDays { get; set; }
        public int? MedicineQuantity { get; set; }

    }
}
