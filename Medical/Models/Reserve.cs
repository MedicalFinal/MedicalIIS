using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Reserve
    {
        public Reserve()
        {
            CaseRecords = new HashSet<CaseRecord>();
        }

        public int ReserveId { get; set; }
        public int ClinicDetailId { get; set; }
        public int? State { get; set; }
        public int MemberId { get; set; }
        public DateTime ReserveDate { get; set; }
        public string RemarkPatient { get; set; }
        public string RemarkAdmin { get; set; }
        public int? Source { get; set; }
        public int? SequenceNumber { get; set; }

        public virtual ClinicDetail ClinicDetail { get; set; }
        public virtual Member Member { get; set; }
        public virtual Source SourceNavigation { get; set; }
        public virtual State StateNavigation { get; set; }
        public virtual ICollection<CaseRecord> CaseRecords { get; set; }
    }
}
