using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class SignalRController : Controller
    {

        public IActionResult Index()
        {
            return View();
            //int user = 0;
            //if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            //{
            //    CMemberAdminViewModel vm = null;

            //    string logJson = "";
            //    logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
            //    vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);


            //    user =(int)vm.Role;
            //}
            
            //return View(user);
        }
        
    }
}
