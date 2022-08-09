using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ArticleComment
    {
        public int CommentId { get; set; }
        public int? ArticleId { get; set; }
        public int? MemberId { get; set; }
        public int? DoctorId { get; set; }
        public string CommentContent { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Article Article { get; set; }
        public virtual Member Member { get; set; }
    }
}
