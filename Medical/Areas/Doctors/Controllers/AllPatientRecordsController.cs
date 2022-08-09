using Medical.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Medical.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Medical.Controllers
{
    [Area(areaName: "Doctors")]
    public class AllPatientRecordsController : Controller
    {
        private readonly MedicalContext _context;
        public AllPatientRecordsController(MedicalContext medicalContext)
        {
            _context = medicalContext;
        }
        public IActionResult List()
        {
            CMemberAdminViewModel vm = null;
            string logJson = "";
            logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
            vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
            var id = vm.Member.MemberId;
            TempData["DoctorName"] = vm.MemberName;
            var doctorID = _context.Doctors.Where(m => m.MemberId == id ).Select(m => m.DoctorId).FirstOrDefault();
            var reserveID = _context.CaseRecords.Include(x => x.TreatmentDetail).Include(x => x.Member).Include(x => x.Reserve).ThenInclude(x => x.ClinicDetail).ThenInclude(x => x.Doctor).
               Where(x => x.Reserve.ClinicDetail.DoctorId == doctorID);
            return View(reserveID.ToList());
        }
        public IActionResult Edit(int? id)
        {
            CaseRecord caseRecord = _context.CaseRecords.Include(x => x.TreatmentDetail).Include(x => x.Member).Include(x => x.Reserve).ThenInclude(x => x.ClinicDetail).ThenInclude(x => x.Doctor).FirstOrDefault(c => c.CaseId == id);
            return View(caseRecord);
        } 
        [HttpPost]
        public IActionResult Edit(CaseRecord c) 
        {
            CaseRecord caseRecord = _context.CaseRecords.FirstOrDefault(cd => cd.CaseId == c.CaseId);
            if(caseRecord != null)
            {
                caseRecord.CaseId = c.CaseId;
                caseRecord.DiagnosticRecord = c.DiagnosticRecord;
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //查找醫生
        public IActionResult SearchMem(string m)
        {
            var id = m /*vm.Member.MemberId*/;
            var doctorID = _context.Doctors.Where(m => m.Member.MemberName == id).Select(m => m.DoctorId).FirstOrDefault();
            var mem = _context.CaseRecords.Include(x => x.TreatmentDetail).Include(x => x.Member).Include(x => x.Reserve).ThenInclude(x => x.ClinicDetail).ThenInclude(x => x.Doctor).
               Where(x => x.Reserve.ClinicDetail.DoctorId == doctorID).Select(m => m.Member.MemberName);
            return Json(mem);
        }
    }
}
