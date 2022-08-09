using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CityId { get; set; }
        public string ShipAddress { get; set; }
        public bool? IsPaid { get; set; }
        public int? OrderStateId { get; set; }
        public int? PayTypeId { get; set; }
        public int? ShipTypeId { get; set; }
        public int? CouponDetailId { get; set; }

        public virtual City City { get; set; }
        public virtual CouponDetail CouponDetail { get; set; }
        public virtual Member Member { get; set; }
        public virtual Orderstate OrderState { get; set; }
        public virtual Paytype PayType { get; set; }
        public virtual ShipType ShipType { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
