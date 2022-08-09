using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Medical.Models;
using Medical.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Medical.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Medical.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly MedicalContext _medicalContext;

        public HomeController(ILogger<HomeController> logger, MedicalContext medicalContext)
        {
            _logger = logger;
            _medicalContext = medicalContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

      



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
