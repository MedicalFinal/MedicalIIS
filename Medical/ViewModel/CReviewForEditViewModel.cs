using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CReviewForEditViewModel
    {
        public List<Product> productList { get; set; }
        public List<Member> memberList { get; set; }
        public List<Review> reviewList { get; set; }
        public List<RatingType> ratingTypeList { get; set; }

    }
}
