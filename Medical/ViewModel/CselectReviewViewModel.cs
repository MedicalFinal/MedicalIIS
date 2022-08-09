using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CselectReviewViewModel
    {
        public bool shade { get; set; }
        public int revId { get; set; }
        public string datetimeStr { get; set; }
        public string memberName { get; set; }
        public int ratingtypeNum { get; set; }
        public string productName { get; set; }
        public string CommentContent { get; set; }
    }
}
