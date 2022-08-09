using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Administarator
    {
        public Administarator()
        {
            Advertises = new HashSet<Advertise>();
            Articles = new HashSet<Article>();
            News = new HashSet<News>();
        }

        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminAccount { get; set; }
        public string AdminPassword { get; set; }

        public virtual ICollection<Advertise> Advertises { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
