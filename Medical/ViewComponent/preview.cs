using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewComponents
{
    public class preview:ViewComponent
    {
        private readonly MedicalContext _medicalContext;
        public preview(MedicalContext medicalContext)
        {
            _medicalContext = medicalContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(CClinicDetailAdminViewModel cVM)
        {
            if (cVM == null)
                return View(null);

            DateTime dtForm = DateTime.Parse(cVM.dateForm);
            DateTime dtTo = DateTime.Parse(cVM.dateTo);
            int[] day = cVM.day;
            int[] time = cVM.time;
            TimeSpan Diff_dates = dtTo.Subtract(dtForm);
            int count = 0;

            List<CClinicDetailAdminViewModel> list = new List<CClinicDetailAdminViewModel>();
                
            for (int i = 0; i < Diff_dates.Days+1 ; i++)
            {
                DateTime dt = dtForm.AddDays(i);

                if (DateTime.Now.Date.CompareTo(dt.Date) < 0)
                {
                    if (dt.DayOfWeek != DayOfWeek.Saturday || dt.DayOfWeek != DayOfWeek.Sunday)
                    {
                        foreach (var d in day)
                        {
                            if ((int)dt.DayOfWeek == d)
                            {
                                foreach (var t in time)
                                {
                                    CClinicDetailAdminViewModel cc = new CClinicDetailAdminViewModel();
                                    cc.DoctorId = cVM.DoctorId;
                                    cc.doctorName = _medicalContext.Doctors.Where(x => x.DoctorId.Equals(cVM.DoctorId)).SingleOrDefault().DoctorName;
                                    cc.DepartmentId = (int)cVM.DepartmentId;
                                    cc.deptName = _medicalContext.Departments.Where(x => x.DepartmentId.Equals(cVM.DepartmentId)).SingleOrDefault().DeptName;
                                    cc.ClinicDate = dt;
                                    cc.PeriodId = t;
                                    cc.periodName = _medicalContext.Periods.Where(x => x.PeriodId.Equals(t)).SingleOrDefault().PeriodDetail;
                                    cc.RoomId = cVM.RoomId;
                                    cc.roomName = _medicalContext.ClinicRooms.Where(x => x.RoomId.Equals(cVM.RoomId)).SingleOrDefault().RoomName;
                                    cc.Online = 0;

                                    var qry = _medicalContext.ClinicDetails.Where(x =>
                                    x.ClinicDate.Value.Month.Equals(dt.Date.Month) && x.ClinicDate.Value.Day.Equals(dt.Date.Day) && x.PeriodId.Equals(t) && x.RoomId.Equals(cVM.RoomId) ||
                                    x.ClinicDate.Value.Month.Equals(dt.Date.Month) && x.ClinicDate.Value.Day.Equals(dt.Date.Day) && x.PeriodId.Equals(t) && x.DoctorId.Equals(cVM.DoctorId)
                                    );
                                    if (qry.Count() > 0)
                                    {
                                        cc.repeat = true;
                                        count++;
                                    }
                                    else
                                    {
                                        cc.repeat = false;
                                    }

                                    list.Add(cc);
                                }
                            }
                        }
                    }
                }
            }

            TempData["repeatKey"] = count.ToString();
            return View(list);
        }
    }
}
