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
    public class NNewsFrontController : Controller
    {
        private readonly MedicalContext _medicalContext;
        public NNewsFrontController(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(NNewsSearchKeywordViewModel vModel)
        {
            List<NNewsViewModel> list = new List<NNewsViewModel>();
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                var data = _medicalContext.News.Include(n => n.NewsCategory);
                foreach(var n in data)
                {
                    NNewsViewModel news = new NNewsViewModel();
                    news.news = n;
                    list.Add(news);
                }
            }
            else
            {
                var data = _medicalContext.News.Include(n => n.NewsCategory).
                    Where(n => n.NewsTitle.Contains(vModel.txtKeyword) ||
                    n.NewsCategory.NewsCategoryName.Equals(vModel.txtKeyword) 
                );
                foreach(var n in data)
                {
                    NNewsViewModel news = new NNewsViewModel();
                    news.news = n;
                    list.Add(news);
                }
            }
            return View(list.ToList());
        }
        public IActionResult ListDetail(int? id)
        {
            News n = _medicalContext.News.FirstOrDefault(x => x.No == id);
            if (n == null)
            {
                return RedirectToAction("List");
            }
            return View(n);
        }
        
    }
}
