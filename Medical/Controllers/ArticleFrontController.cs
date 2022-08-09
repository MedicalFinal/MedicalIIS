using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class ArticleFrontController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly MedicalContext _medicalContext;
        public ArticleFrontController(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }
        public IActionResult List(AArticleSearchKeywordViewModel vModel)
        {

            List<AArticleViewModel> list = new List<AArticleViewModel>();

            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                var datas = _medicalContext.Articles.Include(x => x.Doctor);
                foreach (var a in datas)
                {
                    AArticleViewModel ar = new AArticleViewModel();
                    ar.article = a;
                    list.Add(ar);
                }
            }
            else
            {
                var datas = _medicalContext.Articles.Include(x => x.Doctor).Where(a =>
                a.Articeltitle.Contains(vModel.txtKeyword) ||
                a.Doctor.DoctorName.Contains(vModel.txtKeyword));
                foreach (var a in datas)
                {
                    AArticleViewModel ar = new AArticleViewModel();
                    ar.article = a;
                    list.Add(ar);
                }
            }
            return View(list.ToList());
        }
        public IActionResult AdvertiseChange()
        {
            var data = _medicalContext.Advertises.Select(a => a.AdPicturePath);
            return Json(data);
        }
        public IActionResult ListDetail(int? id)
        {
            Article a = _medicalContext.Articles.Include(x=>x.Doctor).FirstOrDefault(x => x.ArticleId == id);
            if (a == null)
            {
                return RedirectToAction("List");
            }
            return View(a);
        }
    }
}
