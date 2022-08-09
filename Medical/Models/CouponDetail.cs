using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class CouponDetail
    {
        public CouponDetail()
        {
            Orders = new HashSet<Order>();
        }

        public int CouponDetailId { get; set; }
        public int CouponId { get; set; }
        public int MemberId { get; set; }
        public bool CouponUsed { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
