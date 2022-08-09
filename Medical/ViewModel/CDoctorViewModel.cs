using Medical.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Medical.ViewModels
{
    public class CDoctorViewModel
    {
        private Doctor doc;
        public CDoctorViewModel()
        {
            doc = new Doctor();
        }
        public Doctor doctor
        {
            get { return doc; }
            set { doc = value; }
        }
        public int DoctorId {
            get { return doc.DoctorId; }
            set { doc.DoctorId = value; }
        }
        public int? MemberId {
            get { return doc.MemberId; }
            set { doc.MemberId = value; }
        }
        [DisplayName("醫師姓名")]
        public string DoctorName {
            get { return doc.DoctorName; }
            set { doc.DoctorName = value; }
        }
        [DisplayName("專長")]
        public int? DepartmentId {
            get { return doc.DepartmentId; }
            set { doc.DepartmentId = value; } }
        [DisplayName("學歷")]
        public string Education {
            get { return doc.Education; }
            set { doc.Education = value; }
        }
        [DisplayName("職稱")]
        public string JobTitle {
            get { return doc.JobTitle; }
            set { doc.JobTitle = value; }
        }
        //public string PicturePath {
        //    get { return doc.PicturePath; }
        //    set { doc.PicturePath = value; }
        //}


        public IFormFile photo { get; set; }
    }
}
