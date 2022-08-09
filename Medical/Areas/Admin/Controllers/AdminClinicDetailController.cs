using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Medical.Controllers.calendar;

namespace Medical.Controllers
{
    [Area(areaName: "Admin")]
    public class AdminClinicDetailController : Controller
    {
        private readonly MedicalContext _medicalContext;
        public AdminClinicDetailController(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddClinic()
        {
            return View();
        }

        public IActionResult Preview(CClinicDetailAdminViewModel cVM)
        {
            return ViewComponent("preview", new { cVM = cVM });
        }

        public IActionResult loadData()
        {
            List<detail> list = new List<detail>();

            var qry = _medicalContext.ClinicDetails.Include(x => x.Doctor).Include(x => x.Room).Include(x => x.Period).Include(x => x.Department);

            foreach (var i in qry)
            {
                int dt = (i.ClinicDate).Value.Hour;
                string color;

                if (dt == 9)
                { color = "#0073b7"; }
                else if (dt == 13)
                { color = "#FBBC05"; }
                else
                { color = "#34A853"; }

                calendar.detail detail = new calendar.detail
                {
                    id = i.ClinicDetailId,
                    start = ((DateTime)(i.ClinicDate)).ToString("yyyy-MM-dd HH:00:00"),
                    end = (((DateTime)(i.ClinicDate)).AddHours(3)).ToString("yyyy-MM-dd HH:00:00"),
                    title = $"{i.Doctor.DoctorName} ({i.Room.RoomName})",
                    borderColor = color,
                    backgroundColor = color,
                    textColor = "white",
                    color = color,
                    constraint = "businessHours",
                    extendedProps = new calendar.ExtendedProps
                    {
                        doctorId = i.DoctorId,
                        doctorName = i.Doctor.DoctorName,
                        deptId = i.DepartmentId,
                        deptName = i.Department.DeptName,
                        roomId = i.RoomId,
                        roomName = i.Room.RoomName,
                        periodId = i.PeriodId,
                        periodDetail = i.Period.PeriodDetail,
                    }
                };
                list.Add(detail);
            }
            return Json(list);
        }

        public IActionResult CountClinicDetailId()
        {
            List<CClinicDetailViewModel> list = new List<CClinicDetailViewModel>();
            var count = _medicalContext.ClinicDetails.Count().ToString();
            return Content(count, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Create(CClinicDetailAdminViewModel[] obj)
        {
            string result = "新增失敗"; 

            if (obj != null)
            {
                foreach (var i in obj)
                {
                    DateTime dt = Convert.ToDateTime(i.dateForm);

                    if (DateTime.Now.Date.CompareTo(dt.Date) < 0)
                    {
                        var qry = _medicalContext.Periods.Where(x => x.PeriodDetail.Equals(i.periodName)).Single().PeriodId;
                        if (qry == 1)
                        {
                            dt = dt.AddHours(9);
                        }
                        else if (qry == 2)
                        {
                            dt = dt.AddHours(13);
                        }
                        else if (qry == 3)
                        {
                            dt = dt.AddHours(17);
                        }

                        ClinicDetail c = new ClinicDetail()
                        {
                            DoctorId = i.DoctorId,
                            DepartmentId = i.DepartmentId,
                            ClinicDate = dt,
                            PeriodId = qry,
                            RoomId = i.RoomId,
                            Online = 0,
                            LimitNum = 6
                        };

                        _medicalContext.Add(c);
                        _medicalContext.SaveChanges();
                        
                        result = "新增成功";
                    } 
                }
            }
            return Content(result, "text/plain", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public void Update(CClinicDetailViewModel cVM)
        {
            int hour = cVM.ClinicDate.Value.Hour;
            ClinicDetail clinicDetail = _medicalContext.ClinicDetails.Where(x => x.ClinicDetailId.Equals(cVM.clinicDetailId)).FirstOrDefault();

            if (hour == 9)
            { 
                cVM.periodID = 1; 
            }
            else if (hour == 13)
            { 
                cVM.PeriodId = 2; 
            }
            else
            { 
                cVM.periodID = 3; 
            }

            if (clinicDetail != null)
            {
                clinicDetail.DoctorId = cVM.DoctorId;
                clinicDetail.DepartmentId = cVM.DepartmentId;
                clinicDetail.PeriodId = cVM.PeriodId;
                clinicDetail.RoomId = cVM.RoomId;
                clinicDetail.ClinicDate = cVM.ClinicDate;
                _medicalContext.SaveChanges();
            }
        }

        [HttpPost]
        public void updateMimi(CClinicDetailViewModel cVM)
        {
            ClinicDetail clinicDetail = _medicalContext.ClinicDetails.Where(x => x.ClinicDetailId.Equals(cVM.clinicDetailId)).FirstOrDefault();

            if (clinicDetail != null)
            {
                clinicDetail.DoctorId = cVM.DoctorId;
                clinicDetail.DepartmentId = cVM.DepartmentId;
                clinicDetail.RoomId = cVM.RoomId;
                _medicalContext.SaveChanges();
            }
        }

        public IActionResult Delete(string id)
        {
            string result = "";
            ClinicDetail clinicDetail = _medicalContext.ClinicDetails.Where(x => x.ClinicDetailId.Equals(int.Parse(id))).FirstOrDefault();

            var qry = _medicalContext.Reserves.Where(x => x.ClinicDetailId.Equals(int.Parse(id)));

            if (qry.Count() > 0)
            {
                result = "false";
            }
            else
            {
                if (clinicDetail != null)
                {
                    _medicalContext.ClinicDetails.Remove(clinicDetail);
                    _medicalContext.SaveChanges();
                    result = "true";
                }
            }
            return Content(result, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Check(CClinicDetailAdminViewModel cVM)
        {
            string result = "此時段";
            
            var qry_room = _medicalContext.ClinicDetails.Include(x=>x.Room).Where(x => x.ClinicDate.Value.Equals(cVM.ClinicDate) && x.RoomId.Equals(cVM.RoomId));
            var qry_doctor = _medicalContext.ClinicDetails.Include(x=>x.Doctor).Where(x => x.ClinicDate.Value.Equals(cVM.ClinicDate) && x.DoctorId.Equals(cVM.DoctorId));

            if (qry_room.Count() > 0)
                result = $"診間：{qry_room.FirstOrDefault().Room.RoomName} ";
            if (qry_doctor.Count() > 0)
                result += $" {qry_doctor.FirstOrDefault().Doctor.DoctorName}醫師 ";

            result += "已重覆";

            return Content(result, "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Dept()
        {
            var dept = _medicalContext.Departments.Select(x => new { x.DeptName, x.DepartmentId });
            return Json(dept);
        }

        public IActionResult doctorList(int? deptid)
        {
            var doctors = from q in _medicalContext.Doctors select q;
            if (deptid > 0)
            {
                doctors = doctors.Where(x => x.DepartmentId.Equals(deptid));
            }

            doctors.Select(x => new { x.DoctorName, x.DoctorId });

            return Json(doctors);
        }

        public IActionResult roomList()
        {
            var rooms = _medicalContext.ClinicRooms.Select(x => x.RoomName);
            return Json(rooms);
        }

        public DateTime TranDate(string date, string time)
        {
            string[] aryDate = date.Split('/');
            DateTime dt = new DateTime(int.Parse(aryDate[0]), int.Parse(aryDate[1]), int.Parse(aryDate[2]), 0, 0, 0);

            switch (time)
            {
                case "上午時段":
                    dt = dt.AddHours(9);
                    break;
                case "下午時段":
                    dt = dt.AddHours(13);
                    break;
                case "晚上時段":
                    dt = dt.AddHours(17);
                    break;
            }
            return dt;
        }
    }

    class calendar
    {
        public class Data
        {
            public detail[] details { get; set; }
        }
        public class ExtendedProps
        {
            public int? doctorId { get; set; }
            public string doctorName { get; set; }
            public int? deptId { get; set; }
            public string deptName { get; set; }
            public int? roomId { get; set; }
            public string roomName { get; set; }
            public int periodId { get; set; }
            public string periodDetail { get; set; }
        }

        public class detail
        {
            public int id { get; set; }
            //public string url { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string title { get; set; }
            public string textColor { get; set; }
            public string constraint { get; set; }
            public string color { get; set; }
            public string borderColor { get; set; }
            public string backgroundColor { get; set; }
            public ExtendedProps extendedProps { get; set; }
        }

    }
}
