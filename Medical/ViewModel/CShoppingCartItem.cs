using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CShoppingCartItem
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int 小計 { get { return this.prodspec.UnitPrice * this.cart.ProductAmount; }}
        public Product prod{ get; set; }
        public ProductSpecification prodspec { get; set; }
        public ShoppingCart cart { get; set; }

    }
}
