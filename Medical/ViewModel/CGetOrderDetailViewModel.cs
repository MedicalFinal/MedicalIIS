using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CGetOrderDetailViewModel
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string MemberName { get; set; }
        public string ProductName { get; set; }
        public string Phone { get; set; }
        public string ProductImage { get; set; }
        public int UnitPrice { get; set; }
        public int CouponDiscountNum { get; set; }
        public int 小計 { get { return this.UnitPrice * this.Quantity; } }
        public DateTime OrderDate { get; set; }
        public int? OrderStateId { get; set; }

    }
}
