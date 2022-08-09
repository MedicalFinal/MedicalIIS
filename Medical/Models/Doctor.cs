using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Articles = new HashSet<Article>();
            ClinicDetails = new HashSet<ClinicDetail>();
            Experiences = new HashSet<Experience>();
            RatingDoctors = new HashSet<RatingDoctor>();
            Treatments = new HashSet<Treatment>();
        }

        public int DoctorId { get; set; }
        public int? MemberId { get; set; }
        public string DoctorName { get; set; }
        public int? DepartmentId { get; set; }
        public string Education { get; set; }
        public string JobTitle { get; set; }
        public string PicturePath { get; set; }

        public virtual Department Department { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<RatingDoctor> RatingDoctors { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
