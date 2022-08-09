using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleComments = new HashSet<ArticleComment>();
        }

        public int ArticleId { get; set; }
        public int? DoctorId { get; set; }
        public int? AdminId { get; set; }
        public string Articeltitle { get; set; }
        public string ArticleContent { get; set; }
        public string CreateDate { get; set; }
        public string ArPicturePath { get; set; }

        public virtual Administarator Admin { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
    }
}
