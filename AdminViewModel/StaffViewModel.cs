using FinalCMS.Models;
using System.Collections.Generic;
using System;

namespace FinalCMS.AdminViewModel
{
    public class StaffViewModel
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime? StaffDob { get; set; }
        public string StaffGender { get; set; }
        public string StaffAddress { get; set; }
        public string StaffBloodgroup { get; set; }
        public DateTime? StaffJoindate { get; set; }
        public int? StaffSalary { get; set; }
        public long? StaffMobieno { get; set; }
        public string EMail { get; set; }
        public string Qualification1 { get; set; }
        public string Department1 { get; set; }
        public int SpecializationId { get; set; }
        public int LoginId { get; set; }


        public string SpecializationName { get; set; }
        public string RoleName { get; set; }
        public int? ConsultationFee { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int QualificationId { get; set; }

        public int RoleId { get; set; }


    }
}
