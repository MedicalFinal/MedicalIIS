using Medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.ViewModel
{
    public class Clinictime
    {
        private readonly MedicalContext _context;
        public Clinictime(MedicalContext db)
        {
            _context = db;
        }

        public int? clinicDetailid { get; set; }
        public int? online { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).Online; } }

        public int? roomid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).RoomId; } }
        public string roomName { get { return _context.ClinicRooms.FirstOrDefault(n => n.RoomId == roomid).RoomName; } }

        public int? roomnumber { get { return _context.ClinicRooms.FirstOrDefault(n => n.RoomId == roomid).Number; } }


        public int? docid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).DoctorId; } }
        public string docName { get { return _context.Doctors.FirstOrDefault(n => n.DoctorId == docid).DoctorName; } }


        public int? depid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).DepartmentId; } }
        public string depName { get { return _context.Departments.FirstOrDefault(n => n.DepartmentId==depid).DeptName; } }

        //public int? Roomid { get { return _context.ClinicDetails.FirstOrDefault(n => n.ClinicDetailId == clinicDetailid).RoomId; } }
        public int? number { get { return _context.ClinicRooms.FirstOrDefault(n => n.RoomId == roomid).Number; } }
    }
}
