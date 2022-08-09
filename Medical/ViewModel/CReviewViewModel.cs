using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CReviewViewModel
    {
        private Review _review;
        private Member _member;
        private Product _product;
        private ProductSpecification _productSpecification;
        private RatingType _ratingType;
        public CReviewViewModel()
        {
            _review = new Review();
            _member = new Member();
            _product = new Product();
            _ratingType = new RatingType();
            _productSpecification = new ProductSpecification();
        }

        public Review Review
        {
            get { return _review; }
            set { _review = value; }
        }
        public ProductSpecification ProductSpecification
        {
            get { return _productSpecification; }
            set { _productSpecification= value; }
        }
        public string productpicture
        {
            get { return _productSpecification.ProductImage; }
            set { _productSpecification.ProductImage = value; }
        }


        public int ReviewId
        {
            get { return _review.ReviewId; }
            set { _review.ReviewId = value; }
        }
        [DisplayName("評論人")]
        public int MemberId
        {
            get { return _review.MemberId; }
            set { _review.MemberId = value; }
        }
        [DisplayName("產品名稱")]
        public int ProductId
        {
            get { return _review.ProductId; }
            set { _review.ProductId = value; }
        }
        public int? RatingTypeId
        {
            get { return _review.RatingTypeId; }
            set { _review.RatingTypeId = value; }
        }
        [DisplayName("評論內容")]
        public string CommentContent
        {
            get { return _review.CommentContent; }
            set { _review.CommentContent = value; }
        }

        //[DisplayName("日期")]
        //public DateTime? CreateDate
        //{
        //    get { return _review.CreateDate.ToString(); }
        //    set { _review.CreateDate = DateTime.Parse(value); }
        //}

        [DisplayName("評論人")]
        public virtual Member Member
        {
            get { return _member; }
            set { _member = value; }
        }
        public virtual Product Product
        {
            get { return _product; }
            set { _product = value; }
        }
        [DisplayName("評分")]
        public virtual RatingType RatingType
        {
            get { return _ratingType; }
            set { _ratingType = value; }
        }
    }
}
