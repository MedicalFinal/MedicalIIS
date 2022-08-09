using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class COrderforShowViewModel
    {
        public List<Order> orderList { get; set; }
        public List<OrderDetail> orderDetailList { get; set; }
        public List<Product> productList { get; set; }
        public List<Member> memberList { get; set; }
        public List<Orderstate> orderstateList { get; set; }
        public List<Paytype> paytypeList { get; set; }
        public List<ShipType> shipTypeList { get; set; }
        public List<ProductSpecification> productSpecificationList { get; set; }
    }
}
