using Microsoft.AspNetCore.Http;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CShoppingCartViewModel
    {
        private ShoppingCart _shoppingCart;

        public CShoppingCartViewModel()
        {
            _shoppingCart = new ShoppingCart();
        }

        public ShoppingCart shopCart
        {
            get { return _shoppingCart; }
            set { _shoppingCart = value; }
        }


        public Product prod { get; set; }
        public ProductSpecification prodSpec { get; set; }
        public List<Member> memList { get; set; }
        public List<RatingType> ratingList { get; set; }
        public List<Review> prodReviewList { get; set; }
        public List<ProductBrand> brandList { get; set; }
        public List<ProductCategory> cateList { get; set; }

        public List<string> otherP { get; set; }

        public int MemberID
        {
            get { return _shoppingCart.MemberId; }
            set { _shoppingCart.MemberId = value; }
        }

        public int ProductID
        {
            get { return _shoppingCart.ProductId; }
            set { _shoppingCart.ProductId = value; }

        }

        public int ProductAmount
        {
            get { return _shoppingCart.ProductAmount; }
            set { _shoppingCart.ProductAmount = value; }
        }


        public IFormFile photo { get; set; }
    }


}
