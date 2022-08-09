using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class ClinicSearch
    {
        private readonly MedicalContext _context;
        public ClinicSearch(MedicalContext db)
        {
            _context = db;
        }
        //會員專區開始
        public int? memberid { get; set; }

        public string email { get; set; }
        public string membername { get; set; }
        //會員專區結束
        public DateTime? date { get; set; }     
        public int? doctorid { get; set; }
        public int? departmentid { get; set; }
        public int? periodid { get; set; }
        public int? roomid { get; set; }        
        
        public int? clinicDetailid { get; set; }
        //醫生照片
        public string PicturePath { get { return _context.Doctors.FirstOrDefault(n => n.DoctorId == doctorid).PicturePath; } }
        public string clinicdate { get { return ((DateTime)_context.ClinicDetails.FirstOrDefault(n => n.ClinicDate==date).ClinicDate).ToString("yyyy/MM/dd"); } }
        public string doctor { get { return _context.Doctors.FirstOrDefault(n => n.DoctorId == doctorid).DoctorName; } }
        public string department { get { return _context.Departments.FirstOrDefault(n => n.DepartmentId == departmentid).DeptName; } }

        public int? online { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).Online; } }
        
        public string period { get { return _context.Periods.FirstOrDefault(n => n.PeriodId == periodid).PeriodDetail; } }

        public string roomName { get { return _context.ClinicRooms.FirstOrDefault(n => n.RoomId == roomid).RoomName; } }

        public int? number { get { return _context.ClinicRooms.FirstOrDefault(n => n.RoomId == roomid).Number; } }
        //剩餘的可預約人數
        public int? sequence_number 
        { 
            get 
            {
                int? sequence = 0;
                //如果有診 但還沒有人預約 回傳可預約人數6人
                var result= _context.Reserves.Where(n => n.ClinicDetailId == clinicDetailid);
                if (result.Count()==0)
                {
                    sequence= 6;
                }
                foreach (var item in result)
                {
                    //如果有人預約 sequence_number卻是null
                    if (item.SequenceNumber == null)
                    { sequence = 6; }
                    //6減最大的sequence_number 等於 剩餘人數
                    sequence = 6 - item.SequenceNumber;                                  
                }
                return sequence;  
            }
             
        }

      
    }
}
