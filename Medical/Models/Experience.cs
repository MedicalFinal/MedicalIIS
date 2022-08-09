using System;
using System.Collections.Generic;

#nullable disable

namespace Medical.Models
{
    public partial class Experience
    {
        public int DoctorId { get; set; }
        public int ExperienceId { get; set; }
        public string Experience1 { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
