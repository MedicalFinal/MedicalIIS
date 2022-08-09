using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class CReserveforShowViewModel
    {
        public List<Doctor> doctorsList { get; set; }
        public List<Department> departmentList { get; set; }
        public List<ClinicDetail> clinicDetailList { get; set; }
        public ClinicDetail clinicDetail { get; set; }
        public List<Reserve> reserveList { get; set; }

        public List<TreatmentDetail> treatmentDetailList { get; set; }

        public List<Period> periodlist { get; set; }
        public List<ClinicRoom> clinicRoomlist { get; set; }

        public string departmenttext { get; set; }

        public string doctortext { get; set; }
        public string treatmentDetailtext { get; set; }


    }
}
