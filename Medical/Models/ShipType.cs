using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ShipType
    {
        public ShipType()
        {
            Orders = new HashSet<Order>();
        }

        public int ShipTypeId { get; set; }
        public string ShipType1 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
