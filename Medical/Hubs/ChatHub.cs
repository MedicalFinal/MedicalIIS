using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Medical.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string sendUser, string message)
        {
            //string user = "";
            //if (sendUser==3||sendUser==2)
            //{
            //    user = "客服";
            //}
            //else 
            //{
            //    user = "訪客";
            //}
            await Clients.All.SendAsync("ReceiveMessage",sendUser,message);
        }

        //public Task sendmessage(string message)
        //{
        //    string user = "";

        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
    }
}
