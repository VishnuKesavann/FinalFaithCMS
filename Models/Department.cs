using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Department
    {
        public Department()
        {
            Specialization = new HashSet<Specialization>();
            Staff = new HashSet<Staff>();
        }

        public int DepartmentId { get; set; }
        public string Department1 { get; set; }

        public virtual ICollection<Specialization> Specialization { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
