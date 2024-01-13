using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            Doctor = new HashSet<Doctor>();
            Staff = new HashSet<Staff>();
        }

        public int SpecializationId { get; set; }
        public int? DepartmentId { get; set; }
        public string SpecializationName { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
