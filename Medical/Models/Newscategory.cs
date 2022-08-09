using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Newscategory
    {
        public Newscategory()
        {
            News = new HashSet<News>();
        }

        public int NewsCategoryId { get; set; }
        public string NewsCategoryName { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
