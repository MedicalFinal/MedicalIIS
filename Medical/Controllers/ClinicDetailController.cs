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
    public class ClinicDetailController : Controller
    {

        private readonly MedicalContext _context;
        public ClinicDetailController(MedicalContext medicalContext)
        {
            _context = medicalContext;
        }



        public IActionResult List()
        {


            ViewBag.Time = DateTime.Now.ToString("yyyy/MM/dd");

            int hour = DateTime.Now.Hour;
            int Period = 0;

            if (hour <= 12)
            {
                Period = 1; //上午時段
            }
            if (hour > 12 && hour < 17)
            {
                Period = 2; //下午時段
            }
            if (hour > 17 && hour < 21)
            {
                Period = 3; //晚上時段
            }
            ViewBag.period = Period;
            var result = _context.ClinicDetails.Where(a => a.ClinicDate.Value.Date.Equals(DateTime.Today.Date))

                .Select(a => new CClinicDetailViewModel {
                    clinicDetail = a,
                    Doctor = a.Doctor,
                    Department = a.Department,
                    Room = a.Room,
                    Period = a.Period

                });



            return View(result);
        }


        public IActionResult Listjson()
             {
            int hour = DateTime.Now.Hour;
            int Period = 0;

            if (hour <= 12)
            {
                Period = 1; //上午時段
            }
            if (hour > 12 && hour < 17)
            {
                Period = 2; //下午時段
            }
            if (hour > 17 && hour < 21)
            {
                Period = 3; //晚上時段
            }
        
            
            var result = _context.ClinicDetails.Where(a => a.ClinicDate.Value.Date.Equals(DateTime.Today.Date));
            List<Clinictime> list = new List<Clinictime>();
            foreach (var item in result)
            {
                if (item.PeriodId == Period)
                {
                       Clinictime t = new Clinictime(_context)
                    {
                        clinicDetailid=item.ClinicDetailId

                    };
                    list.Add(t); 
                }
                

            }

            return Json(list);
        }
        public IActionResult ListAjax()
        {
            return View();
        }
        
        public IActionResult loadClinicDetail(int period, int addday)   //抓診間 ClinicDetail
        {

            DateTime nowday = DateTime.Now;
            var details = from c in _context.ClinicDetails
                          join d in _context.Doctors on c.DoctorId equals d.DoctorId
                          join p in _context.Periods on c.PeriodId equals p.PeriodId
                          join r in _context.ClinicRooms on c.RoomId equals r.RoomId
                          where c.PeriodId == period && c.ClinicDate.Value.Date <= nowday.AddDays(addday + 6) && c.ClinicDate.Value.Date >= DateTime.Now.Date.AddDays(addday)
                          select new { c.ClinicDetailId, c.DoctorId, d.DoctorName, p.PeriodDetail, c.ClinicDate, r.RoomName };
            
                return Json(details); 
            
        }
        public IActionResult loadReserve(int ClinicID)
        {
            var reserves = from r in _context.Reserves
                           join c in _context.ClinicDetails on r.ClinicDetailId equals c.ClinicDetailId
                           where c.ClinicDetailId == ClinicID
                           select r;
            return Json(reserves);
        }
        


        


    }
}
