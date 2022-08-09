using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class City
    {
        public City()
        {
            Orders = new HashSet<Order>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
