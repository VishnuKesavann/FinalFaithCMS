using System;

namespace FinalCMS.Doctor_Viewmodel
{
    public class Todayapp
    {

        public int AppointmentId { get; set; }
        public int TokenNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string CheckUpStatus { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public string PatientName { get; set; }
        public DateTime PatientDob { get; set; }
        public string Gender { get; set; }
        public int? PatientAge { get; set; }

    }
}
