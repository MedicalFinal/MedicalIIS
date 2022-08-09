using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ClinicRoom
    {
        public ClinicRoom()
        {
            ClinicDetails = new HashSet<ClinicDetail>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int? Number { get; set; }

        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
    }
}
