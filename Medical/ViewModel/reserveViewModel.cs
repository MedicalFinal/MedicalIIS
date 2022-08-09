using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class reserveViewModel
    {
        public string departmentname { get; set; }

        public string doctorname { get; set; }
        public string treatmentDetailname{ get; set; }

        public DateTime? txtdate { get; set; }

        public int clinicDetailid { get; set; }

        public string Remark_Patient { get; set; }

        //代表預約順位
        public int? rank { get; set; }

    }
}
