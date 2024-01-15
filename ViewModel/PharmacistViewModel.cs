namespace FinalCMS.ViewModel
{
    public class PharmacistViewModel
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }
        public long PhoneNumber { get; set; }
        public string PrescribedMedicine { get; set; }
        public string Dosage { get; set; }
        public int? DosageDays { get; set; }
        public int? Quantity { get; set; }
    }
}
