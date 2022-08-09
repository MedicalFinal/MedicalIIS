using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class AdminProductController : Controller
    {

        private readonly MedicalContext db;
        private IWebHostEnvironment _environment;
        public AdminProductController(IWebHostEnvironment myEnvironment, MedicalContext _medical)
        {
            _environment = myEnvironment;
            db = _medical;
        }

        public IActionResult ChooseView()
        {
            return View();
        }
        public IActionResult PowerB()
        {
            return View();
        }

        public IActionResult productManage()
        {
            CProductViewModel model = new CProductViewModel
            {
                productList = db.Products.ToList(),
                brandList = db.ProductBrands.ToList(),
                cateList = db.ProductCategories.ToList(),
                prodSpecList = db.ProductSpecifications.ToList()

            };

            IEnumerable<SelectListItem> brandSelectListItem = (from p in db.ProductBrands
                                                               where p.ProductBrandName != null
                                                               select p).ToList().Select(p => new SelectListItem
                                                               { Value = p.ProductBrandId.ToString(), Text = p.ProductBrandName });

            ViewBag.brandSelectListItem = brandSelectListItem;


            IEnumerable<SelectListItem> cateSelectListItem = (from p in db.ProductCategories
                                                              where p.ProductCategoryName != null
                                                              select p).ToList().Select(p => new SelectListItem
                                                              { Value = p.ProductCategoryId.ToString(), Text = p.ProductCategoryName });

            ViewBag.cateSelectListItem = cateSelectListItem;




            return View(model);
        }

        public IActionResult SelectedProduct(int? id)
        {
            ProductSpecification ps = db.ProductSpecifications.FirstOrDefault(ps => ps.ProductId == id);
            List<OtherProductImage> op = db.OtherProductImages.Where(op => op.ProductId == id).ToList();

            List<string> myop = new List<string>(); ;

            foreach(var a in op)
            {
                string optxt = a.OtherProductPhoto;
                myop.Add(optxt);
            }

            Product p = db.Products.FirstOrDefault(p => p.ProductId == id);

            string pbName = db.ProductBrands.FirstOrDefault(pb => pb.ProductBrandId == p.ProductBrandId).ProductBrandName; 
            string pcName = db.ProductCategories.FirstOrDefault(pc => pc.ProductCategoryId== p.ProductCategoryId).ProductCategoryName;


            CSelectedProductViewModel prod = new CSelectedProductViewModel()
            {


                Shelfdate = p.Shelfdate,
                Stock = p.Stock,
                ProductSpecificationId = ps.ProductSpecificationId,
                ProductId = ps.ProductId,
                ProductAppearance = ps.ProductAppearance,
                ProductColor = ps.ProductColor,
                ProductImage = ps.ProductImage,
                ProductMaterial = ps.ProductMaterial,
                ProductName = ps.Product.ProductName,
                Discontinued = p.Discontinued,
                UnitPrice = ps.UnitPrice,
                ProductBrandName = pbName,
                ProductCategoryName = pcName,
                ProductBrandId = p.ProductBrandId,
                ProductCategoryId = p.ProductCategoryId,
                otherP = myop

            };

            return Json(prod);
        }
        [HttpPost]
        public IActionResult ChangeSave(CSelectedProductViewModel cSelected/*,IFormFile photo*/ /*string myJson*/)
        {
            // CSelectedProductViewModel cSelected = JsonSerializer.Deserialize<CSelectedProductViewModel>(myJson);
            Product mp = db.Products.FirstOrDefault(p => p.ProductId == cSelected.ProductId);
            ProductSpecification mps = db.ProductSpecifications.FirstOrDefault(m => m.ProductSpecificationId == cSelected.ProductSpecificationId);


            if (cSelected.photo != null)
            {
                string mpName = Guid.NewGuid().ToString() + ".jpg";
                cSelected.photo.CopyTo(new FileStream(_environment.WebRootPath + "/images/" + mpName, FileMode.Create));
                mps.ProductImage = mpName;
            }

            mp.Discontinued = cSelected.Discontinued;
            mp.ProductBrandId = cSelected.ProductBrandId;
            mp.ProductCategoryId = cSelected.ProductCategoryId;
            mp.ProductName = cSelected.ProductName;
            mp.Shelfdate = cSelected.Shelfdate;
            mp.Stock = cSelected.Stock;

            mps.ProductAppearance = cSelected.ProductAppearance;
            mps.ProductColor = cSelected.ProductColor;
            mps.ProductMaterial = cSelected.ProductMaterial;
            mps.UnitPrice = cSelected.UnitPrice;
            db.SaveChanges();

            return Content("成功");
        }

        public IActionResult AddNewProd(CSelectedProductViewModel cSelected/*,IFormFile photo*/ /*string myJson*/)
        {
            // CSelectedProductViewModel cSelected = JsonSerializer.Deserialize<CSelectedProductViewModel>(myJson);
            Product mp = new Product();
            ProductSpecification mps = new ProductSpecification();

            if (cSelected.photo != null)
            {
                string mpName = Guid.NewGuid().ToString() + ".jpg";
                cSelected.photo.CopyTo(new FileStream(_environment.WebRootPath + "/images/" + mpName, FileMode.Create));
                mps.ProductImage = mpName;
            }

            mp.Discontinued = cSelected.Discontinued;
            mp.ProductBrandId = cSelected.ProductBrandId;
            mp.ProductCategoryId = cSelected.ProductCategoryId;
            mp.ProductName = cSelected.ProductName;
            mp.Shelfdate = cSelected.Shelfdate;
            mp.Stock = cSelected.Stock;
            mp.Cost = cSelected.Cost;
            db.Products.Add(mp);
            db.SaveChanges();

            mps.ProductAppearance = cSelected.ProductAppearance;
            mps.ProductColor = cSelected.ProductColor;
            mps.ProductMaterial = cSelected.ProductMaterial;
            mps.UnitPrice = cSelected.UnitPrice;
            mps.ProductId = mp.ProductId;
            db.ProductSpecifications.Add(mps);

            db.SaveChanges();


            return Content(mp.ProductId.ToString());
        }


        public IActionResult test64(string [] multipleImgsArray,string productBeforeName)  //多圖 base64傳送
        {
            if(multipleImgsArray.Length==0 || productBeforeName == "")
            {
                return Content("失敗");
            }
            int count = 0;
            List<OtherProductImage> others = db.OtherProductImages.Where(o => o.Product.ProductName == productBeforeName).ToList();
            int pId = db.Products.FirstOrDefault(p => p.ProductName == productBeforeName).ProductId;
            if (others.Count == 0)
            {
                foreach (var arr in multipleImgsArray)
                {

                    byte[] bit = Convert.FromBase64String(arr);
                    MemoryStream ms = new MemoryStream(bit);
                    Bitmap bmp = new Bitmap(ms);
                    string mpName = Guid.NewGuid().ToString() + ".jpg";
                    string FilePath = _environment.WebRootPath + "/images/" + mpName;

                    bmp.Save(FilePath, ImageFormat.Jpeg);

                    OtherProductImage other = new OtherProductImage();
                    other.ProductId = pId;
                    other.OtherProductPhoto = mpName;
                    db.OtherProductImages.Add(other);
                    db.SaveChanges();
                    count++;
                }
            }
            else
            {
                foreach(var arr in multipleImgsArray)
                {
                
                    byte[] bit = Convert.FromBase64String(arr);
                    MemoryStream ms = new MemoryStream(bit);
                    Bitmap bmp = new Bitmap(ms);
                    string mpName = Guid.NewGuid().ToString() + ".jpg";
                    string FilePath = _environment.WebRootPath + "/images/" + mpName;

                    bmp.Save(FilePath, ImageFormat.Jpeg);

                    others[count].OtherProductPhoto = mpName;

                    //OtherProductImage other = new OtherProductImage();
                    //other.ProductId = 1;
                    //other.OtherProductPhoto = mpName;
                    db.SaveChanges();
                    count++;
                } 
            }

            return Content("成功");
        }


        // 新增商品
        public IActionResult AddNewProduct()
        {
            IEnumerable<SelectListItem> brandSelectListItem = (from p in db.ProductBrands
                                                               where p.ProductBrandName != null
                                                               select p).ToList().Select(p => new SelectListItem
                                                               { Value = p.ProductBrandId.ToString(), Text = p.ProductBrandName });

            ViewBag.brandSelectListItem = brandSelectListItem;


            IEnumerable<SelectListItem> cateSelectListItem = (from p in db.ProductCategories
                                                              where p.ProductCategoryName != null
                                                              select p).ToList().Select(p => new SelectListItem
                                                              { Value = p.ProductCategoryId.ToString(), Text = p.ProductCategoryName });

            ViewBag.cateSelectListItem = cateSelectListItem;



            return View();
        }

        // 刪除/下架商品 cshtml
        public IActionResult RemoveProduct()
        {
            CProductViewModel model = new CProductViewModel
            {
                productList = db.Products.ToList(),
                brandList = db.ProductBrands.ToList(),
                cateList = db.ProductCategories.ToList(),
                prodSpecList = db.ProductSpecifications.ToList()

            };

            return View(model);
        }
        // 下架多筆商品
        public IActionResult MultipleDiscontinue(string[] multipleD)
        {

            foreach(var prod in multipleD)
            {
                Product p = db.Products.FirstOrDefault(pN => pN.ProductName == prod);
                p.Discontinued = true;
                db.SaveChanges();
            }


            return Content("成功");
        }
        // 下架單筆
        public IActionResult SingleDiscontinue(string singleD)
        {

            Product p = db.Products.FirstOrDefault(pN => pN.ProductName == singleD);
            p.Discontinued = true;
            db.SaveChanges();

            return Content("成功");

        }
        // 上架多筆商品
        public IActionResult MultipleContinue(string[] multipleD)
        {

            foreach (var prod in multipleD)
            {
                Product p = db.Products.FirstOrDefault(pN => pN.ProductName == prod);
                p.Discontinued = false;
                db.SaveChanges();
            }


            return Content("成功");
        }
        // 上架單筆
        public IActionResult SingleContinue(string singleD)
        {

            Product p = db.Products.FirstOrDefault(pN => pN.ProductName == singleD);
            p.Discontinued = false;
            db.SaveChanges();



            return Content("成功");
        }

        // ==========================刪除單筆商品=============================
        public IActionResult SingleProductDelete(string singleD)
        {

            Product p = db.Products.FirstOrDefault(pN => pN.ProductName == singleD);

            ProductSpecification ps = db.ProductSpecifications.FirstOrDefault(pps => pps.ProductId == p.ProductId);
            List<OtherProductImage> otherpList = db.OtherProductImages.Where(otp => otp.ProductId == p.ProductId).ToList();
            int[] oplist = db.OtherProductImages.Where(o => o.ProductId == p.ProductId).Select(op => op.ProductId).ToArray();
            int[] cartlist = db.ShoppingCarts.Where(c => c.ProductId == p.ProductId).Select(cc => cc.ProductId).ToArray();
            int[] orderlist = db.OrderDetails.Where(od => od.ProductId == p.ProductId).Select(odd => odd.ProductId).ToArray();
            int[] reviewlist = db.Reviews.Where(r => r.ProductId == p.ProductId).Select(odd => odd.ProductId).ToArray();


            //Array.Exists<int>(oplist, x => x == p.ProductId);
            var mybool1 = oplist.Contains<int>(p.ProductId);
            var mybool2 = cartlist.Contains<int>(p.ProductId);
            var mybool3 = orderlist.Contains<int>(p.ProductId);
            var mybool4 = reviewlist.Contains<int>(p.ProductId);


            if (mybool2 || mybool3 || mybool4== true)
            {
                return Content("失敗");
            }
            else
            {
                if (mybool1 == true)
                {
                    foreach (var otherp in otherpList)
                    {
                        db.OtherProductImages.Remove(otherp);
                        db.SaveChanges();
                    }
                }

                db.ProductSpecifications.Remove(ps);
                db.SaveChanges();
                db.Products.Remove(p);
                db.SaveChanges();

                return Content("成功");
            }
        }
        // 查詢訂單 主頁
        public IActionResult QueryAllOrders()
        {
            var orderlist = db.Orders.OrderByDescending(o=>o.OrderId).ToList();
            var memlist = db.Members.ToList();

            return View(orderlist);
        }

        public IActionResult GetOrderDetail(int?id)
        {
            // orderID MemberName
            // (od List) Image productName amount 小計 
            // 折價券 折扣數
            var productSlist = db.ProductSpecifications.ToList();
            List<CGetOrderDetailViewModel> odVMList = new List<CGetOrderDetailViewModel>();
            var odlist = db.OrderDetails.Include(od => od.Order).Include(od => od.Product).Include(od => od.Order.Member).Include(od => od.Order.CouponDetail.Coupon).Where(od => od.OrderId == id).ToList();
            
            foreach(var od in odlist)
            {
                CGetOrderDetailViewModel odVM = new CGetOrderDetailViewModel();

                if (od.Order.CouponDetailId != null)
                {
                    odVM.CouponDiscountNum = od.Order.CouponDetail.Coupon.CouponDiscountNum;                   
                };

                odVM.MemberName = od.Order.Member.MemberName;
                odVM.ProductName = od.Product.ProductName;
                odVM.ProductImage = od.Product.ProductSpecifications.FirstOrDefault(ps => ps.ProductId == od.Product.ProductId).ProductImage;
                odVM.UnitPrice = od.Product.ProductSpecifications.FirstOrDefault(ps => ps.ProductId == od.Product.ProductId).UnitPrice;
                odVM.Quantity = od.Quantity;
                odVM.OrderId = od.OrderId;
                odVM.Phone = od.Order.Member.Phone;
                odVM.OrderDate = (DateTime)od.Order.OrderDate;
                odVM.OrderStateId = od.Order.OrderStateId;

                odVMList.Add(odVM);
            }
            
            return Json(odVMList);
        }

        public IActionResult checkOrder(int[] multipleD)
        {

            if (multipleD.Length == 0)
                return Content("失敗");


            foreach (var oId in multipleD)
            {
                Order o = db.Orders.FirstOrDefault(o => o.OrderId == oId);
                o.OrderStateId = 2;
                db.SaveChanges();
            }


            return Content("成功");
        }


        // 評論查詢/刪除 主頁
        public IActionResult DeleteReviews()
        {
            CReviewForEditViewModel cReviewModel = new CReviewForEditViewModel
            {
                productList = db.Products.ToList(),
                memberList = db.Members.ToList(),
                ratingTypeList = db.RatingTypes.ToList(),
                reviewList = db.Reviews.ToList()
            };
            return View(cReviewModel);
        }

        public IActionResult SelectedReview(int? id)
        {
            Review review = db.Reviews.FirstOrDefault(r => r.ReviewId == id);
            string memName = db.Members.FirstOrDefault(m => m.MemberId == review.MemberId).MemberName;
            string ratingstr = db.RatingTypes.FirstOrDefault(rm => rm.RatingTypeId == review.RatingTypeId).RatingTypeName;
            string prodName = db.Products.FirstOrDefault(p => p.ProductId == review.ProductId).ProductName;
            int ratingNum = int.Parse(ratingstr);
            CselectReviewViewModel cReviewmodel = new CselectReviewViewModel()
            {
                shade = (bool)review.Shade,
                revId = review.ReviewId,
                productName = prodName,
                ratingtypeNum = ratingNum,
                memberName = memName,
                CommentContent = review.CommentContent,
                datetimeStr = ((DateTime) review.CreateDate).ToString("yyyy年MM月dd日 hh時mm分"),
            };


            return Json(cReviewmodel);
        }
        public IActionResult SingleReviewDelete(string singleR)
        {
            
            //Product p = db.Products.FirstOrDefault(pN => pN.ProductName == singleD);

            Review review = db.Reviews.FirstOrDefault(r => r.ReviewId == int.Parse(singleR));

            if (review != null)
            {
                review.Shade=true;
                db.SaveChanges();
                return Content("成功");
            }
            else
                return Content("失敗");
        }
        public IActionResult SingleReviewRelieve(string singleR)
        {

            //Product p = db.Products.FirstOrDefault(pN => pN.ProductName == singleD);

            Review review = db.Reviews.FirstOrDefault(r => r.ReviewId == int.Parse(singleR));

            if (review != null)
            {
                review.Shade = false;
                db.SaveChanges();
                return Content("成功");
            }
            else
                return Content("失敗");
        }
        public IActionResult MultipleShade(int[] multipleD)
        {
            if (multipleD.Length == 0)
                return Content("失敗");


            foreach (var revId in multipleD)
            {
                Review r = db.Reviews.FirstOrDefault(rv => rv.ReviewId == revId);
                r.Shade = true;
                db.SaveChanges();
            }


            return Content("成功");
        }

        // 退貨訂單
        public IActionResult CouponList()
        {
            CCounponForShowViewModel couponVM = new CCounponForShowViewModel
            {
                couponDetaillist = db.CouponDetails.ToList(),
                couponlist = db.Coupons.ToList()
            };

            return View(couponVM);
        }

        public IActionResult SelectedCoupon(int?id)
        {
            Coupon c = db.Coupons.FirstOrDefault(cp => cp.CouponId == id);
            return Json(c);
        }

        public IActionResult AddNewCoupon(int reqNum , int discountNum)
        {
            if (discountNum <= 0)
                return Content("失敗");

            Coupon coupon = new Coupon();
            coupon.CouponDiscountNum = discountNum;
            coupon.CouponRequireNum = reqNum;
            db.Coupons.Add(coupon);
            db.SaveChanges();


            return Content((coupon.CouponId).ToString());
        }
    }
}
