using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CGetUserCoupon
    {

        public int CouponDetailId { get; set; }
        public int CouponId { get; set; }
        public int MemberId { get; set; }
        public int CouponRequireNum { get; set; }
        public int CouponDiscountNum { get; set; }
    }
}
