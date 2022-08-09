using Medical.Class;
using Medical.Hubs;
using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class HomeController : Controller
    {
        private readonly Dashboard dashboard;
        public HomeController(Dashboard dashboard)
        {
            this.dashboard = dashboard;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(dashboard.GetAllProducts());
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USE);
                return  RedirectToAction("Index", "Home", new { Area = "" });
            }
            else if (HttpContext.Session.Keys.Contains(CDictionary.SK_GOOGLELOGINED_USE))
            {
                HttpContext.Session.Remove(CDictionary.SK_GOOGLELOGINED_USE);
                return  RedirectToAction("Index", "Home", new { Area = "" });
            }
            return  RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
