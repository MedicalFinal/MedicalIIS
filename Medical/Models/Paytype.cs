using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Paytype
    {
        public Paytype()
        {
            Orders = new HashSet<Order>();
        }

        public int PayTypeId { get; set; }
        public string PayType1 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
