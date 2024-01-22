namespace FinalCMS.Doctor_Viewmodel
{
    public class Patientview
    {

        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int PatientAge { get; set; }
        public string BloodGroup { get; set; }
        public long PhoneNumber { get; set; }
        public string CheckUpStatus { get; set; }

    }
}
