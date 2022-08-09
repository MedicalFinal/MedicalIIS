using Medical.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    
    public class AArticleViewModel
    {
        private Doctor _doctor;
        private Article _article;
        public AArticleViewModel()
        {
            _article = new Article();
            _doctor = new Doctor();
        }
        public Article article
        {
            get { return _article; }
            set { _article = value; }
        }
        public int ArticleId
        {
            get { return _article.ArticleId; }
            set { _article.ArticleId = value; }
        }
        [Required(ErrorMessage = "*此欄必填*")]
        [DisplayName("醫生")]
        public int? DoctorId
        {
            get { return _article.DoctorId; }
            set { _article.DoctorId = value; }
        }
        public int? AdminId
        {
            get { return _article.AdminId; }
            set { _article.AdminId = value; }
        }
        [DisplayName("疾病")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string Articeltitle
        {
            get { return _article.Articeltitle; }
            set { _article.Articeltitle = value; }
        }
        [DisplayName("文章內容")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string ArticleContent
        {
            get { return _article.ArticleContent; }
            set { _article.ArticleContent = value; }
        }
        public string ArPicturePath
        {
            get { return _article.ArPicturePath; }
            set { _article.ArPicturePath = value; }
        }
        public IFormFile photo { get; set; }
        [DisplayName("發佈日期")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string CreateDate
        {
            get { return _article.CreateDate; }
            set { _article.CreateDate = value; }
        }
        public virtual Administarator Admin
        {
            get { return _article.Admin; }
            set { _article.Admin = value; }
        }
        [Required(ErrorMessage = "*此欄必填*")]
        [DisplayName("醫生")]
        public virtual Doctor Doctor
        {
            get { return _article.Doctor; }
            set { _article.Doctor = value; }
        }
        public virtual ICollection<ArticleComment> ArticleComments
        {
            get { return _article.ArticleComments; }
            set { _article.ArticleComments = value; }
        }
    }
}
