using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Medical.Class;
using Medical.Controllers;
using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Medical.Hubs
{
    public class CallHub :Hub
    {
        public async Task SendRooomNo(string clinicId, string count)
        {
            var room = Call.GetRoomName(int.Parse(clinicId));
            var no = Call.GetSquNo(int.Parse(clinicId), int.Parse(count));
            await Clients.All.SendAsync("ReceivedNumber", room, no);
        }
    }
}
