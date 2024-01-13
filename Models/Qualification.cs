using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Qualification
    {
        public Qualification()
        {
            Staff = new HashSet<Staff>();
        }

        public int QualificationId { get; set; }
        public string Qualification1 { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
