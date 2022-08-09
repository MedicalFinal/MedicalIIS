using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CAddReviewItem
    {
        public int ReviewId { get; set; }
        public int MemberId { get; set; }
        public int? ProductId { get; set; }
        public int? RatingTypeId { get; set; }
        public string CommentContent { get; set; }
        public string CreateDate { get; set; }

        public Product product { get; set; }
    }
}
