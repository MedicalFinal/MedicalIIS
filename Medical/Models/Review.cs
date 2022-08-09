using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int? RatingTypeId { get; set; }
        public string CommentContent { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Shade { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
        public virtual RatingType RatingType { get; set; }
    }
}
