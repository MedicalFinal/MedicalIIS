using Medical.Hubs;
using Medical.Models;
using Medical.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Medical.Class
{
    public class Call 
    {
        public static int GetRoomName(int id)
        {
            MedicalContext medicalContext = new MedicalContext();
            var qry = medicalContext.ClinicDetails.Include(x => x.Room).Where(x => x.ClinicDetailId.Equals(id)).SingleOrDefault().Room.RoomName;
            return int.Parse(qry);
        }

        public static int GetSquNo(int id, int memberId)
        {
            MedicalContext medicalContext = new MedicalContext();
            var qry = medicalContext.Reserves.Where(x => x.ClinicDetailId.Equals(id) && x.MemberId.Equals(memberId)).FirstOrDefault().SequenceNumber;
            return (int)qry;
        }

    }
}
