using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ClinicDetail
    {
        public ClinicDetail()
        {
            Reserves = new HashSet<Reserve>();
        }

        public int ClinicDetailId { get; set; }
        public int? DoctorId { get; set; }
        public int? DepartmentId { get; set; }
        public int PeriodId { get; set; }
        public int? Online { get; set; }
        public int? RoomId { get; set; }
        public DateTime? ClinicDate { get; set; }
        public int? LimitNum { get; set; }

        public virtual Department Department { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Period Period { get; set; }
        public virtual ClinicRoom Room { get; set; }
        public virtual ICollection<Reserve> Reserves { get; set; }
    }
}
