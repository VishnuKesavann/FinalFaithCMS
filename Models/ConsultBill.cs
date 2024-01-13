using System;
using System.Collections.Generic;

namespace FinalCMS.Models
{
    public partial class ConsultBill
    {
        public int BillId { get; set; }
        public int AppointmentId { get; set; }
        public decimal? RegisterFees { get; set; }
        public decimal TotalAmt { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
