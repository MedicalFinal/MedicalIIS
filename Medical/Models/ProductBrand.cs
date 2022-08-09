using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ProductBrand
    {
        public ProductBrand()
        {
            Products = new HashSet<Product>();
        }

        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
