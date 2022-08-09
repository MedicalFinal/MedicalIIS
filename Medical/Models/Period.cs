using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Period
    {
        public Period()
        {
            ClinicDetails = new HashSet<ClinicDetail>();
        }

        public int PeriodId { get; set; }
        public string PeriodDetail { get; set; }

        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
    }
}
