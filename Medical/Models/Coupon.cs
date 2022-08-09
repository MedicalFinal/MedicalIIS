using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            CouponDetails = new HashSet<CouponDetail>();
        }

        public int CouponId { get; set; }
        public int CouponRequireNum { get; set; }
        public int CouponDiscountNum { get; set; }

        public virtual ICollection<CouponDetail> CouponDetails { get; set; }
    }
}
