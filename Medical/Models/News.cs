using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class News
    {
        public int No { get; set; }
        public int? AdminId { get; set; }
        public int? NewsCategoryId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string NewsPicturePath { get; set; }

        public virtual Administarator Admin { get; set; }
        public virtual Newscategory NewsCategory { get; set; }
    }
}
