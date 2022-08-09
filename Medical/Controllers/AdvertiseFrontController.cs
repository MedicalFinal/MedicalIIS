using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class AdvertiseFrontController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly MedicalContext _medicalContext;
        public AdvertiseFrontController(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }
        public IActionResult List(AAdvertiseSearchKeywordViewModel vModel)
        {
            
            List<AAdvertiseViewModel> list = new List<AAdvertiseViewModel>();

            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                var datas = _medicalContext.Advertises;
                foreach (var a in datas)
                {
                    AAdvertiseViewModel ad = new AAdvertiseViewModel();
                    ad.advertise = a;
                    list.Add(ad);
                }
            }
            else
            {
                var datas = _medicalContext.Advertises.Where(a =>
                a.AdTitle.Contains(vModel.txtKeyword));
                foreach (var a in datas)
                {
                    AAdvertiseViewModel ad = new AAdvertiseViewModel();
                    ad.advertise = a;
                    list.Add(ad);
                }
            }
            return View(list.ToList());
        }
        public IActionResult ListDetail(int? id)
        {
            Advertise a = _medicalContext.Advertises.FirstOrDefault(x => x.No == id);
            if (a == null)
            {
                return RedirectToAction("List");
            }
            return View(a);
        }
    }

}
