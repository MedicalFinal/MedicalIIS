using Microsoft.AspNetCore.Http;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CProductViewModel
    {
        public List<Product> productList { get; set; }
        public List<ProductBrand> brandList { get; set; }
        public List<ProductCategory> cateList { get; set; }

        public List<ProductSpecification> prodSpecList { get; set; }


        private Product _prod;
        private ProductSpecification _productSpec;
        private ProductBrand _productBrand;
        private ProductCategory _productCategory;
        public CProductViewModel()
        {
            _prod = new Product();
            _productSpec = new ProductSpecification();
            _productBrand = new ProductBrand();
            _productCategory = new ProductCategory();
        }

        public Product product
        {
            get { return _prod; }
            set { _prod = value; }
        }

        public ProductSpecification productSpec
        {
            get { return _productSpec; }
            set { _productSpec = value; }
        }
        public ProductBrand productBrand
        {
            get { return _productBrand; }
            set { _productBrand = value; }
        }

        public ProductCategory productCate
        {
            get { return _productCategory; }
            set { _productCategory = value; }
        }
        public int ProductId
        {
            get { return _prod.ProductId; }
            set { _prod.ProductId = value; _productSpec.ProductId = value; }
        }

        public int ProductCategoryId
        {
            get { return _prod.ProductCategoryId; }
            set { _prod.ProductCategoryId = value; }
        }
        [DisplayName("產品種類")]
        public string ProductCategoryName
        {
            get { return _productCategory.ProductCategoryName; }
            set { _productCategory.ProductCategoryName = value; }
        }
        public int ProductBrandId
        {
            get { return _prod.ProductBrandId; }
            set { _prod.ProductBrandId = value; }
        }

        [DisplayName("產品品牌")]
        public string ProductBrandName
        {
            get { return _productBrand.ProductBrandName; }
            set { _productBrand.ProductBrandName = value; }
        }

        [DisplayName("產品名稱")]
        public string ProductName
        {
            get { return _prod.ProductName; }
            set { _prod.ProductName = value; }
        }
        [DisplayName("庫存量")]
        public int Stock
        {
            get { return _prod.Stock; }
            set { _prod.Stock = value; }
        }
        [DisplayName("停產")]
        public bool Discontinued
        {
            get { return _prod.Discontinued; }
            set { _prod.Discontinued = value; }
        }
        [DisplayName("有效期限")]
        public int Shelfdate
        {
            get { return _prod.Shelfdate; }
            set { _prod.Shelfdate = value; }
        }

        public int ProductSpecificationId
        {
            get { return _productSpec.ProductSpecificationId; }
            set { _productSpec.ProductSpecificationId = value; }
        }
        [DisplayName("產品售價")]
        public int UnitPrice
        {
            get { return _productSpec.UnitPrice; }
            set { _productSpec.UnitPrice = value; }
        }
        [DisplayName("產品圖")]  //Path
        public string ProductImage
        {
            get { return _productSpec.ProductImage; }
            set { _productSpec.ProductImage = value; }
        }

        [DisplayName("顏色")]
        public string ProductColor
        {
            get { return _productSpec.ProductColor; }
            set { _productSpec.ProductColor = value; }
        }
        [DisplayName("外觀描述")]
        public string ProductAppearance
        {
            get { return _productSpec.ProductAppearance; }
            set { _productSpec.ProductAppearance = value; }
        }
        [DisplayName("材質")]
        public string ProductMaterial
        {
            get { return _productSpec.ProductMaterial; }
            set { _productSpec.ProductMaterial = value; }
        }

        public IFormFile photo { get; set; }


    }
}
