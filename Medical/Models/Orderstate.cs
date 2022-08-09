using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Orderstate
    {
        public Orderstate()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderStateId { get; set; }
        public string OrderState1 { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
