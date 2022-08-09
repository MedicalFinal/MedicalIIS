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
    [Area(areaName:"Admin")]
    public class AdvertiseAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IWebHostEnvironment _host;

        private readonly MedicalContext _medicalContext;
        public AdvertiseAdminController(MedicalContext medicalContext, IWebHostEnvironment hostEnvironment)
        {
            _medicalContext = medicalContext;
            _host = hostEnvironment;
        }

        public IActionResult AdstatueCheckBox()
        {
            var data = _medicalContext.AdvertiseStatues.Select(a => a.Adstatue).Distinct();
            return Json(data);
        }

        public IActionResult List(AAdvertiseSearchKeywordViewModel vModel)
        {
            List<AAdvertiseViewModel> list = new List<AAdvertiseViewModel>();
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                var data = _medicalContext.Advertises.Include(x => x.Adstatue);
                foreach (var a in data)
                {
                    AAdvertiseViewModel ar = new AAdvertiseViewModel();
                    ar.advertise = a;
                    list.Add(ar);
                }
            }
            else
            {
                var data = _medicalContext.Advertises.Where(a =>
                a.AdTitle.Contains(vModel.txtKeyword) ||
                a.AdContant.Contains(vModel.txtKeyword));
                foreach (var a in data)
                {
                    AAdvertiseViewModel ar = new AAdvertiseViewModel();
                    ar.advertise = a;
                    list.Add(ar);
                }
            }

            return View(list.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Advertise ad, AAdvertiseViewModel a)
        {
            if (a.photo != null)
            {
                string path = Path.Combine(_host.WebRootPath, "uploads", a.photo.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    a.photo.CopyTo(fileStream);
                    a.AdPicturePath = a.photo.FileName;
                }
            }

            ad.AdminId = 1;
            ad.AdTitle = a.AdTitle;
            ad.Adstatue = a.Adstatue;
            ad.AdContant = a.AdContant;
            ad.AdPicturePath = a.AdPicturePath;

            _medicalContext.Advertises.Add(ad);
            _medicalContext.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? id)
        {
            Advertise advertise = _medicalContext.Advertises.FirstOrDefault(a => a.No == id);
            if (advertise != null)
            {
                _medicalContext.Advertises.Remove(advertise);
                _medicalContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            AAdvertiseViewModel ad = new AAdvertiseViewModel();
            ad.advertise = _medicalContext.Advertises.Include(x => x.Adstatue).FirstOrDefault(a => a.No == id);
            if (ad == null)
            {
                return RedirectToAction("List");
            }
            return View(ad);
        }
        [HttpPost]
        public IActionResult Edit(AAdvertiseViewModel a)
        {
            Advertise ad = _medicalContext.Advertises.FirstOrDefault(t => t.No == a.No);
            if (ad != null)
            {
                if (a.photo != null)
                {
                    //string aName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(_host.WebRootPath, "uploads", a.photo.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        a.photo.CopyTo(fileStream);
                        a.AdPicturePath = a.photo.FileName;
                    }
                }
                ad.AdminId = a.AdminId;
                ad.AdTitle = a.AdTitle;
                ad.Adstatue = a.Adstatue;
                ad.AdContant = a.AdContant;
                ad.AdPicturePath = a.AdPicturePath;
                _medicalContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
