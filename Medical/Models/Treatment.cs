using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Treatment
    {
        public int TreatmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? TreatmentDetailId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual TreatmentDetail TreatmentDetail { get; set; }
    }
}
