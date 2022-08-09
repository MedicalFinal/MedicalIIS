using Medical.Controllers;
using Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CClinicDetailViewModel
    {

        private ClinicDetail _clinicDetail;
        private Department _department;
        private Reserve _reserve;
        private Doctor _doctor;
        private Period _period;
        private ClinicRoom _clinicRoom;

        public CClinicDetailViewModel()
        {
            _clinicDetail = new ClinicDetail();
            _reserve = new Reserve();
        }
        public ClinicDetail clinicDetail
        {
            get { return _clinicDetail; }
            set { _clinicDetail = value; }
        }

        public int ClinicDetailId
        {
            get { return _clinicDetail.ClinicDetailId; }
            set { _clinicDetail.ClinicDetailId = value; }
        }
        public int? DoctorId
        {
            get { return _clinicDetail.DoctorId; }
            set { _clinicDetail.DoctorId = value; }
        }
        public int? DepartmentId
        {
            get { return _clinicDetail.DepartmentId; }
            set { _clinicDetail.DepartmentId = value; }
        }
        public int PeriodId
        {
            get { return _clinicDetail.PeriodId; }
            set { _clinicDetail.PeriodId = value; }
        }
        public int? Online
        {
            get { return _clinicDetail.Online; }
            set { _clinicDetail.Online = value; }
        }
        public int? RoomId
        {
            get { return _clinicDetail.RoomId; }
            set { _clinicDetail.RoomId = value; }
        }

        public DateTime? ClinicDate
        {
            get { return _clinicDetail.ClinicDate; }
            set { _clinicDetail.ClinicDate = value; }
        }
        [DisplayName("預約上限")]
        public int? LimitNum
        {
            get { return _clinicDetail.LimitNum; }
            set { _clinicDetail.LimitNum = value; }
        }

        public string doctorname
        {
            set; get;
        }
        public string clinicId
        {
            set; get;
        }
      
        public int periodID
        {
            set; get;
        }

        public int roomID
        {
            set; get;
        }

        public DateTime date
        {
            set; get;
        }

        public int clinicDetailId
        {
            set; get;
        }

        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }
    
        public virtual Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        public virtual Period Period
        {
            get { return _period; }
            set { _period = value; }
        }
        public virtual ClinicRoom Room
        {
            get { return _clinicRoom; }
            set { _clinicRoom = value; }
        }
        public virtual Reserve Reserves
        {
            get { return _reserve; }
            set { _reserve = value; }
        }

        



    }
}
