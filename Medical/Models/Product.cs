using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OtherProductImages = new HashSet<OtherProductImage>();
            ProductSpecifications = new HashSet<ProductSpecification>();
            Reviews = new HashSet<Review>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public bool Discontinued { get; set; }
        public int Shelfdate { get; set; }
        public int Cost { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OtherProductImage> OtherProductImages { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
