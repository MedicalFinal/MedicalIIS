using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CCounponForShowViewModel
    {
       public List<Coupon> couponlist { get; set; }
       public List<CouponDetail> couponDetaillist { get; set; }
    }
}
