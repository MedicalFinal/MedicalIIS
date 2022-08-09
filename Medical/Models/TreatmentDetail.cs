using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class TreatmentDetail
    {
        public TreatmentDetail()
        {
            CaseRecords = new HashSet<CaseRecord>();
            Treatments = new HashSet<Treatment>();
        }

        public int TreatmentDetailId { get; set; }
        public string TreatmentDetail1 { get; set; }

        public virtual ICollection<CaseRecord> CaseRecords { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
