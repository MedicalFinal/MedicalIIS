using Microsoft.AspNetCore.Mvc;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical.ViewModels;
using Medical.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Medical.Controllers
{
    public class PatientRecordController : Controller
    {
        private readonly MedicalContext _context;
        public PatientRecordController(MedicalContext medicalContext)
        {
            _context = medicalContext;
        }


        //顯示醫生放入 select
        //public IActionResult Doctor()
        //{
        //    //CMemberAdminViewModel vm = null;
        //    string logJson = "";
        //    logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
        //    //vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
        //    var id = 4/*vm.Member.MemberId*/;
        //    var doc = _context.CaseRecords.Where(m => m.MemberId == id).Select(m => m.Reserve.ClinicDetail.Doctor.DoctorName).Distinct();
        //    return Json(doc);
        //}

        //public IActionResult TreatmentDetail(string doc)
        //{
        //    var ttd = _context.CaseRecords.Where(d => d.Reserve.ClinicDetail.Doctor.DoctorName == doc).Distinct();
        //    return Json(ttd);
        //}

        //public IActionResult SerachDoc(string doc)
        //{
        //    IEnumerable<CaseRecordViewModel> list = null;
        //    var id = 4;
        //    list = _context.CaseRecords.Where(m => m.Reserve.ClinicDetail.Doctor.DoctorName == doc && m.MemberId == id).Select(m => new CaseRecordViewModel
        //    {
        //        Reserve = m.Reserve,
        //        caseRecord = m,
        //        Member = m.Member,
        //        TreatmentDetail = m.TreatmentDetail,
        //        Doctor = m.Reserve.ClinicDetail.Doctor
        //    });
        //    return Json(list);
        //}

        public IActionResult List()
        {
            CMemberAdminViewModel vm = null;
            string logJson = "";
            logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
            if (logJson != null)
            {
            vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
            var id = vm.Member.MemberId;
            IEnumerable<CaseRecordViewModel> list = null;
            
                list = _context.CaseRecords.Where(m => m.MemberId == id).Select(m => new CaseRecordViewModel
                {
                    Reserve = m.Reserve,
                    caseRecord = m,
                    Member = m.Member,
                    TreatmentDetail = m.TreatmentDetail,
                    Doctor = m.Reserve.ClinicDetail.Doctor,
                });
                return View(list);
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Create(int? id)
        {

            Doctor list = _context.Doctors.FirstOrDefault(m => m.DoctorId == id);
            if (list == null)
                return RedirectToAction("Login", "Home");
            return View(list);
        }
        [HttpPost]
        public IActionResult Create(RatingDoctor r)
        {

            _context.RatingDoctors.Add(r);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
