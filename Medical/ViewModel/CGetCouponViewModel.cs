using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CGetCouponViewModel
    {
        private CouponDetail _couponDetail;
        private Coupon _coupon;
        
        public CGetCouponViewModel() 
        {
            _coupon = new Coupon();
            _couponDetail = new CouponDetail();
        
        }

        public CouponDetail couponDetail
        {
            get { return _couponDetail; }
            set { _couponDetail = value; }
        }
        public Coupon coupon
        {
            get { return _coupon; }
            set { _coupon = value; }
        }

        public int CouponDetailId 
        {
            get { return _couponDetail.CouponDetailId; }
            set { _couponDetail.CouponDetailId = value; } 
        }
        public int CouponId
        {
            get { return _couponDetail.CouponDetailId; }
            set { _couponDetail.CouponDetailId = value; }
        }
        public int MemberId
        {
            get { return _couponDetail.MemberId; }
            set { _couponDetail.MemberId = value; }
        }
        public int MemId { get; set; }

        public bool CouponUsed
        {
            get { return _couponDetail.CouponUsed; }
            set { _couponDetail.CouponUsed = value; }
        }


        public int CouponRequireNum
        {
            get { return _coupon.CouponRequireNum; }
            set { _coupon.CouponRequireNum = value; }
        }
        public int CouponDiscountNum
        {
            get { return _coupon.CouponDiscountNum; }
            set { _coupon.CouponDiscountNum = value; }
        }

    }
}
