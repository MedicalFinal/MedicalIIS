using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class AdvertiseStatue
    {
        public AdvertiseStatue()
        {
            Advertises = new HashSet<Advertise>();
        }

        public int AdstatueId { get; set; }
        public string Adstatue { get; set; }

        public virtual ICollection<Advertise> Advertises { get; set; }
    }
}
