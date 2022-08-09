using Medical.Class;
using Medical.Controllers;
using Medical.Models;
using Medical.ViewModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace Medical.Hubs
{
    public class DashboardHub : Hub
    {
        private readonly Dashboard dashboard;
        public DashboardHub(Dashboard dashboard)
        {
            this.dashboard = dashboard;
        }

        public async Task GetAll()
        {
            await Clients.All.SendAsync("ReceivedAll", dashboard.GetMembers(), dashboard.GetOrders(), dashboard.GetReserves(), dashboard.GetRatings(), dashboard.GetAllProducts(), dashboard.GetMonthsOrders(), dashboard.GetTime());
        }


    }
}
