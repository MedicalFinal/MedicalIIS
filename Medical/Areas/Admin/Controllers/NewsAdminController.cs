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

namespace Medical.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class NewsAdminController : Controller
    {
        private readonly IWebHostEnvironment _host;

        private readonly MedicalContext _medicalContext;
        public NewsAdminController(MedicalContext medicalContext, IWebHostEnvironment hostEnvironment)
        {
            _medicalContext = medicalContext;
            _host = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoadCategoryName()
        {
            var data = _medicalContext.Newscategories.Select(n => n.NewsCategoryName).Distinct();
            return Json(data);
        }
        public IActionResult List(NNewsSearchKeywordViewModel vModel)
        {
            
            List<NNewsViewModel> list = new List<NNewsViewModel>();
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                var data = _medicalContext.News.Include(n => n.NewsCategory);
                foreach(var news in data)
                {
                    NNewsViewModel n = new NNewsViewModel();
                    n.news = news;
                    list.Add(n);
                }
            }
            else
            {
                var data = _medicalContext.News.Include(n => n.NewsCategory)
                    .Where(n => n.NewsTitle.Contains(vModel.txtKeyword));
                foreach(var news in data)
                {
                    NNewsViewModel n = new NNewsViewModel();
                    n.news = news;
                    list.Add(n);
                }
            }
            return View(list.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(News news,NNewsViewModel n)
        {
            
            if (n.photo != null)
            {
                string path = Path.Combine(_host.WebRootPath, "uploads", n.photo.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    n.photo.CopyTo(fileStream);
                    n.NewsPicturePath = n.photo.FileName;
                }
            }
            news.AdminId = 1;
            news.NewsTitle = n.NewsTitle;
            news.NewsContent = n.NewsContent;
            news.NewsPicturePath = n.NewsPicturePath;
            news.NewsCategory = n.NewsCategory;
            news.PublishDate = n.PublishDate;
            news.CreateDate = DateTime.Now.Date;


            _medicalContext.News.Add(news);
            _medicalContext.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            News news = _medicalContext.News.FirstOrDefault(a => a.No == id);
            if (news != null)
            {
                _medicalContext.News.Remove(news);
                _medicalContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            NNewsViewModel news = new NNewsViewModel();
            news.news = _medicalContext.News.Include(x => x.NewsCategory).FirstOrDefault(a => a.No == id);
            if (news == null)
            {
                return RedirectToAction("List");
            }
            return View(news);
        }
        [HttpPost]
        public IActionResult Edit(NNewsViewModel n)
        {
            News news = _medicalContext.News.FirstOrDefault(t => t.No == n.No);
            if (news != null)
            {
                if (n.photo != null)
                {
                    //string aName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(_host.WebRootPath, "uploads", n.photo.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        n.photo.CopyTo(fileStream);
                        n.NewsPicturePath = n.photo.FileName;
                    }
                }
                news.AdminId = 1;
                news.NewsTitle = n.NewsTitle;
                news.NewsContent = n.NewsContent;
                news.NewsPicturePath = n.NewsPicturePath;
                news.NewsCategory = n.NewsCategory;
                news.PublishDate = n.PublishDate;
                news.CreateDate = DateTime.Now.Date;

                _medicalContext.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
