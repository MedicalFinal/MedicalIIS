using Medical.Models;
using Medical.ViewModel;
using Medical.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class AdminDoctorController : Controller
    {
        private IWebHostEnvironment _enviroment;
        private readonly MedicalContext _db;
        public AdminDoctorController(IWebHostEnvironment p, MedicalContext db)
        {
            _enviroment = p;
            _db = db;
        }
        public IActionResult Index(CKeyWordViewModel vModel)     //醫生後台主頁
        {

            IEnumerable<Doctor> datas = null;
            if (string.IsNullOrEmpty(vModel.txtKeyword))
            {
                datas = from t in _db.Doctors
                        join d in _db.Departments on t.DepartmentId equals d.DepartmentId
                        select t;
            }
            else
            {
                datas = _db.Doctors.Where(t => t.DoctorName.Contains(vModel.txtKeyword) ||
                t.Education.Contains(vModel.txtKeyword) || t.JobTitle.Contains(vModel.txtKeyword));
            }
            return View(datas);
        }
        public IActionResult checkDN(string dcName)
        {
            Doctor dcCheck = new Doctor();
            var answer = new DocJsonViewModel()
            {
                Answer = ""
            };
            dcCheck = _db.Doctors.FirstOrDefault(d => d.DoctorName == dcName);
            if (dcCheck != null)
                answer.Answer = "該名稱已被使用";
            else
                answer.Answer = "可以使用的名字";
            return Json(answer);
        }
        public IActionResult DocWeb(string docName)
        {
            if (docName == null)
            {
                var docs = _db.Doctors.Where(t => t.DoctorName.Contains(""));
                return Json(docs);
            }
            else
            {
                var docs = _db.Doctors.Where(d => d.DoctorName == docName).Distinct().OrderBy(d => d.DoctorId).Select(a => a);
                return Json(docs);
            }
        }
        public IActionResult Dep()
        {
            var deps = _db.Departments.OrderBy(a=>a.DepartmentId).Select(a => a.DeptName).Distinct();
            return Json(deps);
        }
        public IActionResult Doc(string depName)
        {
            Department seleddep = _db.Departments.FirstOrDefault(b => b.DeptName == depName);
            var docNs = _db.Doctors.Where(d => d.DepartmentId == seleddep.DepartmentId).OrderBy(a=>a.DoctorId).Select(b => b.DoctorName).Distinct();
            return Json(docNs);
        }
        public IActionResult CreateDetail()           //新增醫生資料
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDetail(CDoctorDetailViewModel d) //新增醫生資料
        {
            if (d.photo != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                d.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                d.PicturePath = pName;
            }
            if (d.DepName != null)
            {
                if (_db.Departments.FirstOrDefault(a => a.DeptName == d.DepName) != null)
                {
                    int depID = _db.Departments.FirstOrDefault(a => a.DeptName == d.DepName).DepartmentId;
                    d.DepartmentID = depID;
                }
                else
                {
                    _db.Departments.Add(d.department);
                    _db.SaveChanges();
                    int depID = _db.Departments.FirstOrDefault(a => a.DeptName == d.DepName).DepartmentId;
                    d.DepartmentID = depID;
                }
            }
            _db.Members.Add(d.member);
            _db.SaveChanges();
            d.doctor.MemberId = d.member.MemberId;
            _db.Doctors.Add(d.doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int? id)   //刪除醫生資料
        {

            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            //prod.department = _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            //prod.departmentCategory = _db.DepartmentCategories.FirstOrDefault(t => t.DeptCategoryId == prod.department.DeptCategoryId);
            prod.experience = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            prod.member = _db.Members.FirstOrDefault(t => t.MemberId == prod.doctor.MemberId);
            if (prod != null)
            {
                if (prod.experience != null)
                {
                    _db.Experiences.Remove(prod.experience);
                }
                if (prod.member != null)
                {
                    _db.Members.Remove(prod.member);
                }
                _db.Doctors.Remove(prod.doctor);



                _db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        public IActionResult EditDetail(int? id)           //修改醫生資料
        {
            if (id == null)
                return RedirectToAction("Index");
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            prod.doctor = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            Department dep = _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);            
            if (dep != null)
            {
                prod.department = dep;
            }
            Experience exp = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);            
            if (exp != null)
                prod.experience = exp;
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
        [HttpPost]
        public IActionResult EditDetail(CDoctorDetailViewModel p) //修改醫生資料
        {
            Doctor doc = _db.Doctors.FirstOrDefault(t => t.DoctorId == p.DoctorID);
            Department dep = _db.Departments.FirstOrDefault(s => s.DepartmentId == p.doctor.DepartmentId);
            Experience exp = _db.Experiences.FirstOrDefault(v => v.DoctorId == p.DoctorID);
            Member mem = _db.Members.FirstOrDefault(x => x.MemberId == doc.MemberId);
            if (doc != null)
            {
                if (p.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    p.photo.CopyTo(new FileStream((_enviroment.WebRootPath + "/images/" + pName), FileMode.Create));
                    doc.PicturePath = pName;
                }
                doc.DoctorName = p.DoctorName;
                mem.MemberName = p.DoctorName;
                doc.DepartmentId = p.DepartmentID;
                doc.Education = p.Education;
                doc.JobTitle = p.JobTitle;
            }
            if (p.DepName != null && dep != null)
            {
                if (_db.Departments.Where(t => t.DeptName.Equals(p.DepName)) != null)
                {
                    dep = _db.Departments.FirstOrDefault(t => t.DeptName == p.DepName);
                    doc.DepartmentId = dep.DepartmentId;
                }
                else
                    _db.Departments.Add(p.department);
            }
            if (p.DepName != null && dep == null)
            {
                _db.Departments.Add(p.department);
                _db.SaveChanges();
                dep = _db.Departments.FirstOrDefault(s => s.DeptName == p.DepName);
                doc.DepartmentId = dep.DepartmentId;
            }

            if (exp != null && exp.Experience1 != p.Experience)
            {
                exp.Experience1 = p.Experience;
            }
            if (p.Experience != null && exp == null)
            {
                p.experience.DoctorId = p.doctor.DoctorId;
                _db.Experiences.Add(p.experience);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult Details(int? id)    //醫生詳細資料
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            CDoctorDetailViewModel prod = new CDoctorDetailViewModel();
            Doctor DD = _db.Doctors.FirstOrDefault(t => t.DoctorId == id);
            Experience exp = _db.Experiences.FirstOrDefault(t => t.DoctorId == id);
            prod.doctor = DD;
            if (DD.DepartmentId != null)
                prod.department = _db.Departments.FirstOrDefault(t => t.DepartmentId == prod.doctor.DepartmentId);
            if (exp != null)
                prod.experience = _db.Experiences.FirstOrDefault(t => t.DoctorId == prod.doctor.DoctorId);
            
            if (prod == null)
                return RedirectToAction("Index");
            return View(prod);
        }
        public IActionResult depEdit()   //開發中 Department選單
        {
            return View();
        }
        //public IActionResult getDepC()    //開發中 Department選單
        //{
        //    var depC = _db.DepartmentCategories.Select(a => a.DeptCategoryName).Distinct();
        //    return Json(depC);
        //}
        //public IActionResult getDep(string depCName)   //開發中 Department選單
        //{
        //    DepartmentCategory depCN = _db.DepartmentCategories.FirstOrDefault(b => b.DeptCategoryName == depCName);
        //    var dep = _db.Departments.Where(d => d.DeptCategoryId == depCN.DeptCategoryId).Select(b => b.DeptName).Distinct();
        //    return Json(dep);
        //}
        //[HttpPost]
        //public IActionResult depEdit(CDoctorDetailViewModel deps)
        //{
        //    DepartmentCategory depc = _db.DepartmentCategories.FirstOrDefault(b => b.DeptCategoryName == deps.DeptCategoryName);
        //    Department dep = _db.Departments.FirstOrDefault(b => b.DeptName == deps.DepName);
        //    if (depc != null && dep != null)
        //    {
        //        RedirectToAction("List");
        //    }
        //    if (depc != null && dep == null)
        //    {
        //        _db.Departments.Add(deps.department);
        //    }
        //    if (depc == null)
        //    {
        //        _db.DepartmentCategories.Add(deps.departmentCategory);
        //    }
        //    _db.SaveChanges();
        //    return RedirectToAction("List");
        //}
        //public IActionResult depDelete(CDoctorDetailViewModel deps)
        //{
        //    DepartmentCategory depc = _db.DepartmentCategories.FirstOrDefault(b => b.DeptCategoryName == deps.DeptCategoryName);
        //    Department dep = _db.Departments.FirstOrDefault(b => b.DeptName == deps.DepName);
        //    if (depc != null && dep != null)
        //    {
        //        _db.Departments.Remove(deps.department);
        //    }
        //    if (depc != null && dep == null)
        //    {
        //        _db.DepartmentCategories.Remove(deps.departmentCategory);
        //    }
        //    if (depc == null)
        //    {
        //        return RedirectToAction("List");
        //    }
        //    _db.SaveChanges();
        //    return RedirectToAction("List");
        //}
        public IActionResult Email() //開發中 寄信功能
        {
            return View();
        }

        //==============================冠名======================
        //後台 醫師評論系統  
        //選中那位醫生 進入醫師評論管理 
        public IActionResult DoctorRatinglist(int? id)
        {
            ViewBag.name = _db.Doctors.Where(a => a.DoctorId == id).Select(a => a.DoctorName).FirstOrDefault();
            ViewBag.id = id;
            IEnumerable<CRatingDoctorViewModel> list = null;
            if (id != 0)
            {
                list = _db.RatingDoctors.Where(a => a.DoctorId == id).Select(a => new CRatingDoctorViewModel
                {
                    RatingDoctor = a,
                    Doctor = a.Doctor,
                    RatingType = a.RatingType
                });
            }
            return View(list);
        }




        public IActionResult DoctorRatingEdit(int docid)
        {

            RatingDoctor result = _db.RatingDoctors.Where(a => a.RatingDoctorId == docid).FirstOrDefault();
            if (result.Shade == null)
            {
                result.Shade = true;
                _db.SaveChanges();
                return Content("已遮蔽", "text/plain", Encoding.UTF8);
            }
            else if (result.Shade == true)
            {
                result.Shade = null;
                _db.SaveChanges();
                return Content("正常評論", "text/plain", Encoding.UTF8);
            }
            return Content("null", "text/plain", Encoding.UTF8);
        }

    }
}
