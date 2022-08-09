using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class ReservesSearch
    {
        private readonly MedicalContext _context;
        public ReservesSearch(MedicalContext db)
        {
            _context = db;
        }
        //傳入登入會員ID
        public int? memberid { get; set; }
        //得到預約得診間id
        public int clinicid { get; set; }
                                                                                                                                             
        //得到預約的ID
        public int? reserveid { get { return _context.Reserves.FirstOrDefault(n => n.ClinicDetailId== clinicid && n.MemberId==memberid).ReserveId; } }
        //得到狀態ID
        public int? stateid { get { return _context.Reserves.FirstOrDefault(n => n.ClinicDetailId == clinicid && n.MemberId == memberid).State; } }
        public string state { get { return _context.States.FirstOrDefault(n => n.StateId==stateid ).State1; } }

        //得到專科ID
        public int? depid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicid).DepartmentId; } }
        //醫生
        public int? docid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicid).DoctorId; } }

        //醫生照片
        public string PicturePath { get { return _context.Doctors.FirstOrDefault(n => n.DoctorId == docid).PicturePath; } }
        //時段
        public int? periodid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicid).PeriodId; } }

        public string PeriodDetail { get { return _context.Periods.FirstOrDefault(n => n.PeriodId==periodid).PeriodDetail; } }
        //得到預約的會員備註
        public string Remark_Patient { get { return _context.Reserves.FirstOrDefault(n => n.ClinicDetailId == clinicid && n.MemberId == memberid).RemarkPatient; } }

        //得到預約順位
        public int? sequence_number { get { return _context.Reserves.FirstOrDefault(n => n.ClinicDetailId == clinicid && n.MemberId == memberid).SequenceNumber; } }

        //得到專科名稱
        public string depname { get { return _context.Departments.FirstOrDefault(n => n.DepartmentId== depid).DeptName; } }

        //得到醫生名稱
        public string docname { get { return _context.Doctors.FirstOrDefault(n => n.DoctorId==docid).DoctorName; } }

        //得到門診時間
        public string DateTime { get { return ((DateTime)_context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId==clinicid ).ClinicDate).ToString("yyyy/MM/dd"); } }


    }
}
