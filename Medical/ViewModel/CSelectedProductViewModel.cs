using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Medical.Models;
namespace Medical.ViewModel
{
    public class CSelectedProductViewModel
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public bool Discontinued { get; set; }
        public int Shelfdate { get; set; }

        public int ProductSpecificationId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int UnitPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductColor { get; set; }
        public string ProductAppearance { get; set; }
        public string ProductMaterial { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductCategoryName { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductCategoryId { get; set; }
        public int Cost { get; set; }
        public List<string>otherP{get;set;}
        public IFormFile photo { get; set; }

    }
}
