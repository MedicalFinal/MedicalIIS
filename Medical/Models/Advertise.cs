using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Advertise
    {
        public int No { get; set; }
        public int? AdminId { get; set; }
        public string AdTitle { get; set; }
        public string AdContant { get; set; }
        public string AdPicturePath { get; set; }
        public int? AdstatueId { get; set; }

        public virtual Administarator Admin { get; set; }
        public virtual AdvertiseStatue Adstatue { get; set; }
    }
}
