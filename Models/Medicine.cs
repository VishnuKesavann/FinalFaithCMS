using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            MedicinePrescriptions = new HashSet<MedicinePrescriptions>();
        }

        public int MedicineId { get; set; }
        public int MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string CompanyName { get; set; }
        public int? MedicineStock { get; set; }
        public decimal? MedicineUnitPrice { get; set; }

        public virtual ICollection<MedicinePrescriptions> MedicinePrescriptions { get; set; }
    }
}
