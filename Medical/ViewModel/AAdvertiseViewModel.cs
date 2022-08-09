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
    public class AAdvertiseViewModel
    {
        private Advertise _advertise;
        public AAdvertiseViewModel()
        {
            _advertise = new Advertise();
        }
        public Advertise advertise
        {
            get { return _advertise; }
            set { _advertise = value; }
        }
        public int No
        {
            get { return _advertise.No; }
            set { _advertise.No = value; }
        }
        public int? AdminId
        {
            get { return _advertise.AdminId; }
            set { _advertise.AdminId = value; }
        }
        [DisplayName("標題")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string AdTitle
        {
            get { return _advertise.AdTitle; }
            set { _advertise.AdTitle = value; }
        }
        [DisplayName("廣告內容")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string AdContant
        {
            get { return _advertise.AdContant; }
            set { _advertise.AdContant = value; }
        }
        [DisplayName("圖片")]
        [Required(ErrorMessage = "*此欄必填*")]
        public string AdPicturePath
        {
            get { return _advertise.AdPicturePath; }
            set { _advertise.AdPicturePath = value; }
        }
        [DisplayName("廣告照片")]
        [Required(ErrorMessage = "*此欄必填*")]
        public IFormFile photo { get; set; }
        [DisplayName("狀態")]
        [Required(ErrorMessage = "*此欄必填*")]
        public int? AdstatueId
        {
            get { return _advertise.AdstatueId; }
            set { _advertise.AdstatueId = value; }
        }
        public virtual Administarator Admin
        {
            get { return _advertise.Admin; }
            set { _advertise.Admin = value; }
        }
        public virtual AdvertiseStatue Adstatue
        {
            get { return _advertise.Adstatue; }
            set { _advertise.Adstatue = value; }
        }
    }
}
