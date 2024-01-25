namespace FinalCMS.LabViewModel
{
    public class LabTestVM
    {   
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string TestName { get; set; }
        public int? LabPrescId { get; set; }

        public string LabTestStatus
        {
            get; set;
        }


    }
}
