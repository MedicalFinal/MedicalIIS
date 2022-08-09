using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CLinepayConfirmViewModel
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string orderId { get; set; }
        public string transactionId { get; set; }
    }
}
