using Medical.Models;
using Medical.ViewModel;
using Medical.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Medical.Areas.Admin.Controllers
{[Area(areaName: "Admin")]
    public class AdminMemberListController : Controller
    {
        private readonly MedicalContext _context;

        public AdminMemberListController(MedicalContext context)  //注入
        {
            _context = context;
        }
        //======================================================================
        int pageSize = 10;  //1頁10筆
        public IActionResult AdminMemberList(CKeyWordViewModel keyVModel,int? Role,int page=1)   //管理員帳號登入=>會員清單管理
        {
            int currentPage = page < 1 ? 1 : page;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))  
            {
                if (string.IsNullOrEmpty(keyVModel.txtKeyword))    //沒關鍵字時
                {
                    if (Role!=null&&Role!=-1)   //使用下拉選單時
                    {
                        CMemberViewModel memVModel = new CMemberViewModel();

                        memVModel.mem = _context.Members.Where(n => n.Role == Role).ToList();  //清單篩選顯示!!
                        memVModel.roleTypes = _context.RoleTypes.ToList();  //下拉選單&表格權限顯示
                        memVModel.MemGender = _context.Genders.ToList();
                        ////性別名稱顯示，因為view有關連Model.MemGender所以要傳入(否則viewModel的MemGender為null)，無關聯則不需要
                        memVModel.MemCity = _context.Cities.ToList();
                        //return View(memVModel);
                        //=====================改為pagedlist分頁(viewModel建立IPagedList物件)
                        var mempagelist =_context.Members.Where(n => n.Role == Role).ToList();  //清單篩選顯示!!
                        var roletypeslist= _context.RoleTypes.ToList();  //下拉選單&表格權限顯示
                        var memgenderlist = _context.Genders.ToList();
                        var memcitylist= _context.Cities.ToList();
                        memVModel.mempage = mempagelist.ToPagedList(currentPage, pageSize);
                        memVModel.roleTypespage=roletypeslist.ToPagedList(currentPage, pageSize);
                        memVModel.MemGenderpage = memgenderlist.ToPagedList(currentPage, pageSize);
                        memVModel.MemCitypage = memcitylist.ToPagedList(currentPage, pageSize);
                        return View(memVModel);
                    }
                    else
                    {
                        //    CMemberViewModel memVModel = new CMemberViewModel()
                        //    {
                        //        mem = _context.Members.ToList(),
                        //        roleTypes = _context.RoleTypes.ToList(),
                        //        MemGender = _context.Genders.ToList(),
                        //        MemCity = _context.Cities.ToList()
                        //};  
                        //    return View(memVModel);
                        CMemberViewModel memVModel = new CMemberViewModel();
                        memVModel.mem = _context.Members.ToList();
                        memVModel.MemGender = _context.Genders.ToList();
                        memVModel.roleTypes = _context.RoleTypes.ToList();  //下拉選單&表格權限顯示
                        memVModel.MemCity = _context.Cities.ToList();
                        var mempagelist = _context.Members.ToList();  //清單篩選顯示!!
                        var roletypeslist = _context.RoleTypes.ToList();  //下拉選單&表格權限顯示
                        var memgenderlist = _context.Genders.ToList();
                        var memcitylist = _context.Cities.ToList();
                        memVModel.mempage = mempagelist.ToPagedList(currentPage, pageSize);
                        memVModel.roleTypespage = roletypeslist.ToPagedList(currentPage, pageSize);
                        memVModel.MemGenderpage = memgenderlist.ToPagedList(currentPage, pageSize);
                        memVModel.MemCitypage = memcitylist.ToPagedList(currentPage, pageSize);
                        return View(memVModel);
                    }
                 
                }
                else  //有關鍵字時
                {
                    CMemberViewModel VModel = new CMemberViewModel()
                    {
                        mem = _context.Members.Where(t => t.MemberName.Contains(keyVModel.txtKeyword) ||
                          t.Email.Contains(keyVModel.txtKeyword) || t.Phone.Contains(keyVModel.txtKeyword)).ToList(),
                                roleTypes = _context.RoleTypes.ToList(),
                                          //性別.city名稱顯示，因為view有關連Model.MemGender所以要傳入(否則viewModel的MemGender為null)，無關聯則不需要
                        MemGender = _context.Genders.ToList(),
                        MemCity = _context.Cities.ToList()
                    };
                    return View(VModel);
                }

            }
            else   //沒登入或登入失效時
                return RedirectToAction("Index", "Home");
        }



        public IActionResult AdminCreate()
        {
            CMemberViewModel memVModel = new CMemberViewModel()
            {
                mem = _context.Members.ToList(),
                roleTypes = _context.RoleTypes.ToList(),
                MemCity = _context.Cities.ToList(),
                MemGender = _context.Genders.ToList()
            };
            return View(memVModel);
        }
        [HttpPost]
        public IActionResult AdminCreate(CMemberViewModel vModel)
        {
            if (vModel != null)
            {
                _context.Members.Add(vModel.member);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminMemberList", "AdminMemberList");
        }

        public IActionResult Delete(int? id)
        {
            MedicalContext db = new MedicalContext();
            Member mem = db.Members.FirstOrDefault(c => c.MemberId == id);
            if (mem != null)
            {
                db.Members.Remove(mem);
                db.SaveChanges();
            }
            return RedirectToAction("AdminMemberList", "AdminMemberList");
        }

        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                CMemberViewModel memVModel = new CMemberViewModel();
                memVModel.MemberId = _context.Members.FirstOrDefault(c => c.MemberId == id).MemberId;
                //=============================================//單欄項目顯示!!
                memVModel.MemberName = _context.Members.FirstOrDefault(c => c.MemberId == id).MemberName;  
                memVModel.Password= _context.Members.FirstOrDefault(c => c.MemberId == id).Password;
                memVModel.Email = _context.Members.FirstOrDefault(c => c.MemberId == id).Email;
                memVModel.IcCardNo = _context.Members.FirstOrDefault(c => c.MemberId == id).IcCardNo;
                memVModel.IdentityId = _context.Members.FirstOrDefault(c => c.MemberId == id).IdentityId;
                memVModel.Address = _context.Members.FirstOrDefault(c => c.MemberId == id).Address;
                memVModel.BirthDay = _context.Members.FirstOrDefault(c => c.MemberId == id).BirthDay;
                memVModel.Phone = _context.Members.FirstOrDefault(c => c.MemberId == id).Phone;
                //============================================ //下拉選單所選項目顯示
                memVModel.Role = _context.Members.FirstOrDefault(c => c.MemberId == id).Role;
                memVModel.GenderId = _context.Members.FirstOrDefault(c => c.MemberId == id).GenderId;
                memVModel.CityId = _context.Members.FirstOrDefault(c => c.MemberId == id).CityId;
                //============================================
                memVModel.roleTypes = _context.RoleTypes.ToList();  //下拉選單顯示
                memVModel.MemGender = _context.Genders.ToList();
                memVModel.MemCity = _context.Cities.ToList();
                return View(memVModel);
            }
            return RedirectToAction("AdminMemberList", "AdminMemberList");
        }
        [HttpPost]
        public IActionResult Edit(CMemberViewModel vm)
        {
            //_context.Members.Add(vm.member);   //這樣寫會新增一筆
            Member mem = _context.Members.FirstOrDefault(c => c.MemberId == vm.MemberId);//這裡要等於vm.MemberId而不是@Html.ActionLink的id
            if (mem != null)
            {
                mem.MemberId = vm.MemberId;
                mem.MemberName = vm.MemberName; 
                mem.Email = vm.Email;
                mem.IdentityId = vm.IdentityId;
                mem.Password = vm.Password;
                mem.BirthDay = vm.BirthDay;
                mem.GenderId = vm.GenderId;
                mem.IcCardNo = vm.IcCardNo;
                mem.Phone = vm.Phone;         
                mem.Role = vm.Role;
                mem.CityId = vm.CityId;
                mem.Address = vm.Address;


                _context.SaveChanges();
            }


            return RedirectToAction("AdminMemberList","AdminMemberList");
        }

        //=================for Ajax API
        public IActionResult keywordnew(string thekey)
        {
            var keymem = _context.Members.Where(t => t.MemberName.Contains(thekey) ||
                          t.Email.Contains(thekey) || t.Phone.Contains(thekey)).Select(n => n).OrderBy(t => t.MemberId);
            return Json(keymem);

            //var citiesMem = _context.Members.Where(d => d.MemberId == memid).Distinct().OrderBy(d => d.MemberId).Select(s => s);
            //return Json(citiesMem);
        }

        public IActionResult loadnewTbody(int memid)
        {
                //City cityselect = _context.Cities.FirstOrDefault(n => n.CityName == citiName);
                //var citiesMem = _context.Members.Where(d => d.CityId == cityselect.CityId).Distinct().OrderBy(d => d.CityId).Select(a => a);
                var Mem = _context.Members.Where(d => d.MemberId == memid).OrderBy(d => d.MemberId).Select(s => s);
                return Json(Mem);
            
        }

        //====================================
        //public IActionResult keyword(string thekey)
        //{
        //    var keymem = _context.Members.Where(t => t.MemberName.Contains(thekey) ||
        //                  t.Email.Contains(thekey) || t.Phone.Contains(thekey)).Select(n=>n).OrderBy(t=>t.MemberId);
        //    return Json(keymem);

        //    //var citiesMem = _context.Members.Where(d => d.MemberId == memid).Distinct().OrderBy(d => d.MemberId).Select(s => s);
        //    //return Json(citiesMem);
        //}
        //public IActionResult Role()
        //{
        //    var roles = _context.RoleTypes.OrderBy(a => a.Role).Select(a => a.RoleName).Distinct();
        //    return Json(roles);
        //}

        //public IActionResult City(string roName)
        //{
        //    //先改成顯示name
        //    RoleType ro = _context.RoleTypes.FirstOrDefault(b => b.RoleName == roName);
        //    var memid = _context.Members.Where(d => d.Role == ro.Role).OrderBy(a => a.Role).Select(b => b.MemberId).Distinct();  //用ID才是唯一值
        //    return Json(memid);
        //}
        public IActionResult CityWeb(int memid)
        {
            if (memid == null)
            {

                //var cities = _context.Members.Where(t => t.MemberName.Contains(""));  //隨便找個東西塞 反正空的
                var cities = "";
                return Json(cities);
            }
            else
            {
                //City cityselect = _context.Cities.FirstOrDefault(n => n.CityName == citiName);
                //var citiesMem = _context.Members.Where(d => d.CityId == cityselect.CityId).Distinct().OrderBy(d => d.CityId).Select(a => a);
                var citiesMem = _context.Members.Where(d => d.MemberId == memid).Distinct().OrderBy(d => d.MemberId).Select(s => s);
                return Json(citiesMem);
            }
        }
        public IActionResult GetMemWeb(int kity)
        {
            if (kity == null)
            {
                var docs = _context.Members.Where(t => t.MemberName.Contains(""));
                return Json(docs);
            }
            else
            {
                var docs = _context.Members.Where(d => d.MemberId == kity).Distinct().OrderBy(d => d.MemberId).Select(a => a);
                return Json(docs);
            }
        }
        public IActionResult GetGenderName(int gengen)
        {
            var gend = from a in _context.Members
                       join b in _context.Genders on a.GenderId equals b.GenderId
                       where b.GenderId == gengen
                       select b.Gender1;
            return Json(gend);
        }

        public IActionResult GetCityName(int yourCityid)
        {
            var ctjson = from a in _context.Members
                       join b in _context.Cities on a.CityId equals b.CityId
                       where b.CityId == yourCityid
                       select b.CityName;
            return Json(ctjson);
        }

        public IActionResult GetRoleName(int roro)
        {
            var rolejson = from a in _context.Members
                         join b in _context.RoleTypes on a.Role equals b.Role
                         where b.Role == roro
                         select b.RoleName;
            return Json(rolejson);
        }
    }
}
