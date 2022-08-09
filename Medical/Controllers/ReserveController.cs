using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medical.Controllers
{

    public class ReserveController : Controller
    {
        private readonly MedicalContext _context;
        private IQueryable<ClinicDetail> id;
        int memberid;

        public ReserveController(MedicalContext context)
        {
            _context = context;
        }

        // 得到篩選表資料(醫生 專科 門診日期)
        public IActionResult ReserveList(int? id)
        {

            CReserveforShowViewModel datas = null;
            datas = new CReserveforShowViewModel()
            {
                doctorsList = _context.Doctors.ToList(),
                departmentList = _context.Departments.ToList(),
                treatmentDetailList = _context.TreatmentDetails.ToList(),
                clinicDetailList = _context.ClinicDetails.ToList(),
                periodlist = _context.Periods.ToList(),
                clinicRoomlist = _context.ClinicRooms.ToList()
            };
            if (id != null)
                ViewBag.id = id;
            else
                ViewBag.id = 0;
            return View(datas);
        }
        

        //條件查詢門診
        public IActionResult ReserveResult(reserveViewModel result)
        {
            int member = 0;
            int departmentId = _context.Departments.Where(a => a.DeptName == result.departmentname)
                .Select(a => a.DepartmentId).FirstOrDefault();
            int doctorId = _context.Doctors.Where(a => a.DoctorName == result.doctorname)
                .Select(a => a.DoctorId).FirstOrDefault();
            // 還有 result.txtdate
            DateTime? date = result.txtdate;

            CMemberAdminViewModel vm = null;
            string logJson = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {

                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
                member = vm.MemberId;
            }

            id= _context.ClinicDetails.Where(n => n.Online == 0);

            //if (departmentId > 0 && doctorId > 0 && date != null)
            //{
            //    id = id.Where(a => a.ClinicDate.Value.Date >= date && a.ClinicDate.Value.Date < date.Value.AddDays(7))
            //        .Where(a => a.DoctorId == doctorId).Where(a => a.DepartmentId == departmentId);
            //}
            if (departmentId > 0)
            {
                id = id.Where(a => a.DepartmentId == departmentId);
            }
            if (doctorId > 0)
            {
                id = id.Where(a => a.DoctorId == doctorId);
            }
            if (date != null)
            {
                id = id.Where(a => a.ClinicDate.Value.Date >= date && a.ClinicDate.Value.Date < date.Value.AddDays(7));
            }
            else
            {
                id = id.Where(n => n.ClinicDate >= DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7));
            }


            //================================================================

            //if (departmentId == 0 && doctorId == 0 && date == null)
            //{
            //    id = _context.ClinicDetails.Where(n=>n.Online==0).Where(n=>n.ClinicDate>=DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7));
            //}
            //else if (departmentId == 0 && doctorId>0 &&date!=null)
            //{
            //    id = _context.ClinicDetails.Where(a => a.DoctorId == doctorId)
            //     .Where(a => a.ClinicDate.Value.Date >=date&& a.ClinicDate.Value.Date< date.Value.AddDays(7)).Where(n => n.Online == 0);
            //}
            //else if (doctorId == 0 && departmentId>0 &&date!=null)
            //{
            //    id = _context.ClinicDetails.Where(a => a.DepartmentId == departmentId)
            //     .Where(a => a.ClinicDate.Value.Date >= date && a.ClinicDate.Value.Date < date.Value.AddDays(7)).Where(n => n.Online == 0);
            //}
            //else if (date == null &&departmentId >0 && doctorId>0)
            //{
            //    id = _context.ClinicDetails.Where(a => a.DepartmentId == departmentId)
            //     .Where(a => a.DoctorId == doctorId).Where(n => n.Online == 0).Where(n => n.ClinicDate >= DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7));
            //}
            //else if (departmentId == 0 && date == null)
            //{
            //    id = _context.ClinicDetails
            //     .Where(a => a.DoctorId == doctorId).Where(n => n.Online == 0).Where(n => n.ClinicDate >= DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7));
            //}
            //else if (departmentId == 0 && doctorId == 0)
            //{
            //    id = _context.ClinicDetails
            //     .Where(a => a.ClinicDate.Value.Date >= date && a.ClinicDate.Value.Date < date.Value.AddDays(7)).Where(n => n.Online == 0);
            //}
            //else if (date == null && doctorId == 0)
            //{
            //    id = _context.ClinicDetails
            //     .Where(a => a.DepartmentId == departmentId).Where(n => n.Online == 0).Where(n => n.ClinicDate >= DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7));
            //}
            //else
            //{
            //    id = _context.ClinicDetails.Where(a => a.ClinicDate.Value.Date >= date && a.ClinicDate.Value.Date < date.Value.AddDays(7))
            //        .Where(a => a.DoctorId == doctorId).Where(a => a.DepartmentId == departmentId).Where(n => n.Online == 0);
            //}


           

            //if (member > 0)
            //    id = _context.ClinicDetails
            //        .Where(n => n.Online == 0)
            //        .Where(n => n.ClinicDate >= DateTime.Now && n.ClinicDate.Value.Date < DateTime.Now.AddDays(7))
            //        .Include(x => x.Reserves.Where(x => x.MemberId!=member ));



            List<ClinicSearch> list = new List<ClinicSearch>();
          
            foreach (var item in id.OrderBy(n=>n.ClinicDate))
            {
                //var q = _context.Reserves.Where(n => n.ClinicDetailId == item.ClinicDetailId && n.MemberId == member);

                    ClinicSearch t = new ClinicSearch(_context)
                    {
                        doctorid = item.DoctorId,
                        departmentid = item.DepartmentId,
                        periodid = item.PeriodId,
                        roomid = item.RoomId,
                        date = item.ClinicDate,
                        clinicDetailid = item.ClinicDetailId,
                        memberid = member
                    };
                
                list.Add(t);
           


            }
            
            return Json(list);
        }

        //判斷登入狀況  取得門診資料 進入預約畫面 
        public IActionResult GoReserve(reserveViewModel result)
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                CMemberAdminViewModel vm = null;

                string logJson = "";
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);


                int memberid = vm.MemberId;
                string member = vm.MemberName;
                string email = vm.Email;



                var id = _context.ClinicDetails.Where(n => n.ClinicDetailId == result.clinicDetailid);
                List<ClinicSearch> list = new List<ClinicSearch>();
                foreach (var item in id)
                {
                    ClinicSearch t = new ClinicSearch(_context)
                    {
                        doctorid = item.DoctorId,
                        departmentid = item.DepartmentId,
                        periodid = item.PeriodId,
                        roomid = item.RoomId,
                        date = item.ClinicDate,
                        clinicDetailid = item.ClinicDetailId,
                        memberid= memberid,
                        membername= member,
                        email=email
                    };

                    list.Add(t);                  
                }
                return Json(list);
            }

            return Content("null", "text/plain", Encoding.UTF8);  
        }

        //開始預約
        public IActionResult CreateReserve(reserveViewModel result)
        {

            CMemberAdminViewModel vm = null;
            string logJson = "";
            logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
            vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);

            int id = result.clinicDetailid;
            int? sequence_number = result.rank;
            string remark = result.Remark_Patient;          
            int memberid = vm.MemberId;
            DateTime time = DateTime.Now;

            Reserve reserve = new Reserve();
            reserve.ClinicDetailId = id;
            reserve.State = 4;
            reserve.MemberId = memberid;
            reserve.ReserveDate = time;
            reserve.RemarkPatient = remark;
            reserve.Source = 1;
            reserve.SequenceNumber = sequence_number;

            _context.Add(reserve);
            _context.SaveChanges();

            DateTime? clinictime = _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == id).ClinicDate;


            //成功預約後寄信
            //goblwrtefmpunxvt
            try
            {
                MailMessage mmsg = new MailMessage();
                
                mmsg.From = new MailAddress("wangbo841019@gmail.com", "漢克斯眼科");

                mmsg.To.Add(new MailAddress("c121474790@gmail.com"));//應該要抓會員的email
                mmsg.Subject = "漢克斯眼科|預約通知信 ";

                mmsg.Body = "恭喜預約成功! 你的預約日期是: " + clinictime + "\n順位:" + sequence_number+ "號\n備註:" + remark;
                mmsg.IsBodyHtml = true;
                mmsg.BodyEncoding = Encoding.UTF8;
                mmsg.SubjectEncoding = Encoding.UTF8;

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("hankseyes@gmail.com", "goblwrtefmpunxvt");
                    client.Send(mmsg);
                }

                return Content("null", "text/plain", Encoding.UTF8);

            }
            catch
            {
                return Content("Err", "text/plain", Encoding.UTF8);
            }

            //return Content("null", "text/plain", Encoding.UTF8);
        }


        ////查詢自己的預約
        public IActionResult ReserveSearch()
        {
            
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGINED_USE))
            {
                CMemberAdminViewModel vm = null;
                string logJson = "";
                logJson = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USE);
                vm = JsonSerializer.Deserialize<CMemberAdminViewModel>(logJson);
                memberid = vm.MemberId;

                ViewBag.name = _context.Reserves.Where(a => a.MemberId == memberid).Select(a => a.Member.MemberName).FirstOrDefault();
                var id = _context.Reserves.Where(n => n.MemberId == memberid ).Select(n => n.ClinicDetailId);
                List<ReservesSearch> list = new List<ReservesSearch>();
                foreach (var item in id)
                {
                    ReservesSearch t = new ReservesSearch(_context)
                    {
                        clinicid = item,
                        memberid=vm.MemberId

                    };
                    list.Add(t);
                }
                return View(list);
            }
            return RedirectToAction("Login", "Login");
        }



        public IActionResult Delete(int id)
        {
            Reserve result = new Reserve();
            result = _context.Reserves.FirstOrDefault(a => a.ReserveId == id);
            
            if (result != null)
            {
                _context.Reserves.Remove(result);
                _context.SaveChanges();
            }

            return RedirectToAction("ReserveSearch");
        }

        //public IActionResult SendMail() 
        //{
            


        //    return
        //}







    }
}
