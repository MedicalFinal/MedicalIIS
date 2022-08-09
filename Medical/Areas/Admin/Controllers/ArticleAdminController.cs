using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    [Area(areaName: "Admin")]
    public class ArticleAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IWebHostEnvironment _host;

        private readonly MedicalContext _medicalContext;

        public ArticleAdminController(MedicalContext medicalContext, IWebHostEnvironment hostEnvironment)
        {
            _medicalContext = medicalContext;
            _host = hostEnvironment;
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
        public IActionResult CreateDoctorCheckBox()
        {
            var data = _medicalContext.Doctors.Select(x => x.DoctorName).Distinct();
            return Json(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Article ar,AArticleViewModel a)
        {
            if (a.photo != null)
            {
                string path = Path.Combine(_host.WebRootPath, "uploads", a.photo.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    a.photo.CopyTo(fileStream);
                    a.ArPicturePath = a.photo.FileName;
                }
            }
            ar.ArPicturePath = a.ArPicturePath;
            ar.AdminId = 1;
            ar.Articeltitle = a.Articeltitle;
            ar.ArticleContent = a.ArticleContent;
            ar.CreateDate = DateTime.Now.ToString("yyyy/MM/dd");
            ar.Doctor= a.Doctor;

            _medicalContext.Articles.Add(ar);
            _medicalContext.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {

            Article article = _medicalContext.Articles.FirstOrDefault(a => a.ArticleId == id);
            if (article != null)
            {
                _medicalContext.Articles.Remove(article);
                _medicalContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            AArticleViewModel article = new AArticleViewModel();
            article.article = _medicalContext.Articles.Include(x => x.Doctor).FirstOrDefault(a => a.ArticleId == id);
            if (article == null)
            {
                return RedirectToAction("List");
            }
            return View(article);
        }
        [HttpPost]
        public IActionResult Edit(AArticleViewModel a)
        {
            Article ar = _medicalContext.Articles.FirstOrDefault(t => t.ArticleId == a.ArticleId);
            if (ar != null)
            {
                if (a.photo != null)
                {
                    //string aName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(_host.WebRootPath, "uploads", a.photo.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        a.photo.CopyTo(fileStream);
                        a.ArPicturePath = a.photo.FileName;
                    }
                }
                ar.Doctor = a.Doctor;
                ar.AdminId = 1;
                ar.ArPicturePath = a.ArPicturePath;
                ar.CreateDate = DateTime.Now.ToString("yyyy/MM/dd");
                ar.ArticleContent = a.ArticleContent;
                ar.Articeltitle = a.Articeltitle;
            }
            _medicalContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
