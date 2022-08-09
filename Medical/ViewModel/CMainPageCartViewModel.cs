using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CMainPageCartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductAmount { get; set; }
        public int UnitPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int 小計 { get { return this.UnitPrice * this.ProductAmount; } }
    }
}
