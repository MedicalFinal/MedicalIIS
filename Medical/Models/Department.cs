using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Department
    {
        public Department()
        {
            ClinicDetails = new HashSet<ClinicDetail>();
            Doctors = new HashSet<Doctor>();
        }

        public int DepartmentId { get; set; }
        public string DeptName { get; set; }

        public virtual ICollection<ClinicDetail> ClinicDetails { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
