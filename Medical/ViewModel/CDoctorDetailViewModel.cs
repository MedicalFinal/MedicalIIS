using Medical.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Medical.ViewModels
{
    public class CDoctorDetailViewModel
    {
        private Doctor _doc;
        private Department _dep;
        private Experience _exp;
        private Member _memb;
        private Treatment _trt;
        private TreatmentDetail _trtD;
        public CDoctorDetailViewModel()
        {
            _doc = new Doctor();
            _dep = new Department();
            _exp = new Experience();
            _memb = new Member();
            _trt = new Treatment();
            _trtD = new TreatmentDetail();
        }
        public Member member
        {
            get { return _memb; }
            set { _memb = value; }
        }
        public Doctor doctor
        {
            get { return _doc; }
            set { _doc = value; }
        }
        public Department department
        {
            get { return _dep; }
            set { _dep = value; }
        }
        public Experience experience
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public Treatment treatment
        {
            get { return _trt; }
            set { _trt = value; }
        }
        public TreatmentDetail treatmentDetail
        {
            get { return _trtD; }
            set { _trtD = value; }
        }

        public int DoctorID
        {
            get { return _doc.DoctorId; }
            set { _doc.DoctorId = value; }
        }
        public int MemberID
        {
            get
            {
                return _memb.MemberId;
            }
            set
            {
                _memb.MemberId = value;
                _doc.MemberId = value;
            }
        }
        [DisplayName("醫生姓名")]
        public string DoctorName
        {
            get { return _doc.DoctorName; }
            set { _doc.DoctorName = value; }
        }
        public int? DepartmentID
        {
            get { return _doc.DepartmentId; }
            set { _doc.DepartmentId = value; }
        }
        [DisplayName("學歷")]
        public string Education
        {
            get { return _doc.Education; }
            set { _doc.Education = value; }
        }
        [DisplayName("職稱")]
        public string JobTitle
        {
            get { return _doc.JobTitle; }
            set { _doc.JobTitle = value; }
        }
        [DisplayName("大頭照")]
        public string PicturePath
        {
            get { return _doc.PicturePath; }
            set { _doc.PicturePath = value; }
        }
        [DisplayName("經歷")]
        public string Experience
        {
            get { return _exp.Experience1; }
            set { _exp.Experience1 = value; }
        }
        public int ExperienceID
        {
            get { return _exp.ExperienceId; }
            set { _exp.ExperienceId = value; }
        }
        public int ExperienceDocID
        {
            get { return _exp.DoctorId; }
            set { _exp.DoctorId = value; }
        }

        //public int DepartmentID {
        //    get { return department.DepartmentId; }
        //    set { department.DepartmentId = value; }
        //}
        [DisplayName("科別")]
        public string DepName
        {
            get { return _dep.DeptName; }
            set { _dep.DeptName = value; }
        }


        [DisplayName("照片")]
        public IFormFile photo { get; set; }
        [DisplayName("身分證字號")]
        public string IdentityID
        {
            get { return _memb.IdentityId; }
            set { _memb.IdentityId = value; }
        }
        [DisplayName("信箱/帳號")]
        public string Email
        {
            get { return _memb.Email; }
            set { _memb.Email = value; }
        }
        [DisplayName("密碼")]
        public string Password
        {
            get { return _memb.Password; }
            set { _memb.Password = value; }
        }
        [DisplayName("會員名稱")]
        public string MemberName
        {
            get { return _memb.MemberName; }
            set { _memb.MemberName = value; }
        }
        [DisplayName("權限")]
        public int? Role
        {
            get { return _memb.Role; }
            set { _memb.Role = value; }
        }
        public int TreatmentID
        {
            get { return _trt.TreatmentId; }
            set { _trt.TreatmentId = value; }
        }
        public int? TreatmentDetailID
        {
            get { return _trt.TreatmentDetailId; }
            set { _trt.TreatmentDetailId = value; }
        }
        //public int TreatmentDetailID
        //{
        //    get { return _trtD.TreatmentDetailId; }
        //    set { _trtD.TreatmentDetailId = value; }
        //}
        public string TreatmentDetail1
        {
            get { return _trtD.TreatmentDetail1; }
            set { _trtD.TreatmentDetail1 = value; }
        }

    }
}

