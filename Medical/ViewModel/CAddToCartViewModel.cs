using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical.Models;

namespace Medical.ViewModel
{
    public class CAddToCartViewModel
    {
        public int MemberID { get; set; }
        public int txtCount { get; set; }
        public int txtPId { get; set; }
    }
}
