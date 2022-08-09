using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class ProductSpecification
    {
        public int ProductSpecificationId { get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductColor { get; set; }
        public string ProductAppearance { get; set; }
        public string ProductMaterial { get; set; }

        public virtual Product Product { get; set; }
    }
}
