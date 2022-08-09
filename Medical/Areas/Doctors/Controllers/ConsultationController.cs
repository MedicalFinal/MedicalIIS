using Medical.Models;
using Medical.ViewModel;
using Medical.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Areas.Doctors.Controllers
{
    [Area(areaName: "Doctors")]
    public class ConsultationController : Controller
    {
        private readonly MedicalContext _medicalContext;
        public ConsultationController(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }

        CMemberAdminViewModel vm = null;
        string logJson = "";

        public IActionResult List()
        {
            int doctorId = 1;

            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = System.Text.Json.JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
                TempData["DoctorName"] = vm.MemberName;
                doctorId = _medicalContext.Doctors.Where(x => x.MemberId.Equals(vm.MemberId)).SingleOrDefault().DoctorId;
                List<CClinicDetailAdminViewModel> list = new List<CClinicDetailAdminViewModel>();
                var result = _medicalContext.ClinicDetails.Include(x => x.Doctor).Include(x => x.Department)
                    .Include(x => x.Room).Include(x => x.Period).Where(x => x.DoctorId.Equals(doctorId) && x.Online.Equals(0));

                foreach (var c in result)
                {
                    CClinicDetailAdminViewModel cc = new CClinicDetailAdminViewModel();
                    cc.clinicDetail = c;
                    list.Add(cc);
                }
                return View(list.ToList());

            }
            else          
                return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public IActionResult WorkSpace(int id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = System.Text.Json.JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
                TempData["DoctorName"] = vm.MemberName;

                List<CReserveViewModel> list = new List<CReserveViewModel>();
                TempData["ClinicDetailId"] = id;
                var result = _medicalContext.Reserves.Include(x => x.Member).Where(x => x.ClinicDetailId.Equals(id));

                if (result.Count() > 0)
                {
                    foreach (var c in result)
                    {
                        CReserveViewModel cr = new CReserveViewModel();
                        cr.reserve = c;
                        list.Add(cr);
                    }
                }
                return View(list.ToList());
            }
            else
                return RedirectToAction("Index", "Home", new { Area = "" });
         
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult user(int id)
        {
            var user = _medicalContext.CaseRecords.Include(x=>x.Reserve)
                .Where(x => x.MemberId.Equals(id)).Select(x=> new {x.Reserve.ReserveDate, x.DiagnosticRecord });
            return Json(user);
        }

        public PartialViewResult CaseRecord1(CaseRecord record) 
        {
            return PartialView("_PartialCaseRecord",record);
        }

        public IActionResult Save(CaseRecordViewModel record)
        {
            string result = "";

            var qry = _medicalContext.Reserves.Where(x => x.MemberId.Equals(record.MemberId) & x.ClinicDetailId.Equals(record.clinicId)).FirstOrDefault();
            if (!String.IsNullOrEmpty(record.DiagnosticRecord))
            {
                CaseRecord cr = new CaseRecord();
                cr.ReserveId = qry.ReserveId;
                cr.DiagnosticRecord = record.DiagnosticRecord;
                cr.MemberId = record.MemberId;
     
                _medicalContext.CaseRecords.Add(cr);
                _medicalContext.SaveChanges();
                result = "true";
            }
            else
            {
                result = "false";
            }

            return Content(result, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Finish(int id)
        {
            ClinicDetail clinicDetail = _medicalContext.ClinicDetails.Where(x => x.ClinicDetailId.Equals(id)).SingleOrDefault();
            if (clinicDetail!=null)
            {
                clinicDetail.Online = 1;
                _medicalContext.SaveChanges();
            }

            ClinicRoom clinicRoom = _medicalContext.ClinicRooms.Where(x => x.ClinicDetails.Equals(id)).SingleOrDefault();
            if (clinicRoom != null)
            {
                clinicRoom.Number = 0;
                _medicalContext.SaveChanges();
            }

            return RedirectToAction("List", "Consultation");
        }


        public void sendSquNo(int id, int paitentId)
        {
            var qry = _medicalContext.ClinicDetails.Include(x => x.Room).Where(x => x.ClinicDetailId.Equals(id)).SingleOrDefault();
            var qry2 = _medicalContext.Reserves.Where(x => x.MemberId.Equals(paitentId)&&x.ClinicDetailId.Equals(id)).SingleOrDefault().SequenceNumber;
            if (qry!=null & qry2!=null)
            {
                ClinicRoom clinicRoom = _medicalContext.ClinicRooms.Where(x => x.RoomId.Equals(qry.RoomId)).FirstOrDefault();
                if (clinicRoom != null)
                {
                    clinicRoom.Number = qry2;
                    _medicalContext.SaveChanges();
                }
            }
        }


        public IActionResult Logout()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                HttpContext.Session.Remove(CDictionary.SK_LOGINED_USE);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            else if (HttpContext.Session.Keys.Contains(CDictionary.SK_GOOGLELOGINED_USE))
            {
                HttpContext.Session.Remove(CDictionary.SK_GOOGLELOGINED_USE);
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
