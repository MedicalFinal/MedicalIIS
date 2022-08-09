using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class AddReviewViewModel
    {
        public int txtProductid { get; set; }
        public int txtMemberid { get; set; }
        public int txtratingtype { get; set; }
        public string txtcomment { get; set; }

        public string txtdate { get; set; }

    }
}
