using Medical.Hubs;
using Medical.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Medical.Class
{
    public class Dashboard
    {
        private readonly MedicalContext medicalContext;
        private readonly IHubContext<DashboardHub> context;
        public Dashboard(IHubContext<DashboardHub> context, MedicalContext medicalContext)
        {
            this.medicalContext = medicalContext;
            this.context = context;
        }
        
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            var product = medicalContext.Products.Where(x => x.Stock < 5).ToList();

            foreach(var i in product)
                products.Add(i);

            return products;
        }

        private void dbChangeNotification(object sender, SqlNotificationEventArgs e)
        {
            context.Clients.All.SendAsync("refreshProducts", GetAllProducts());
        }

        public string GetTime()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public int GetMembers()
        {
            var qry = medicalContext.Members.CountAsync();
            return qry.Result;
        }
        public int GetOrders()
        {
            var qry = medicalContext.Orders.CountAsync();
            return qry.Result;
        }
        public int GetReserves()
        {
            var qry = medicalContext.Reserves.CountAsync();
            return qry.Result;
        }
        public double GetRatings()
        {
            double total = 0;
            double count = 0; 
            double avg = 0;

            var qry = medicalContext.RatingDoctors;
            foreach(var i in qry)
            {
                count++;
                total += i.RatingTypeId;
            }

            avg = ( total / (count * 5) )*100;
            return avg;
        }

        public int[] GetMonthsOrders()
        {
            int[] array = new int[6];
            int count = 0;
            DateTime dt = DateTime.Now;
            var qry = medicalContext.Orders.Where(x => x.OrderDate.Value <= dt && x.OrderDate.Value > dt.AddMonths(-5))
                .GroupBy(x => new { x.OrderDate.Value.Month })
                .Select(x => new { count = x.Count(), month = x.Key.Month }).ToList();


           for(int i= dt.AddMonths(-5).Month; i<= dt.AddMonths(0).Month; i++)
           {
                array[count] = 0;
                foreach (var j in qry)
                {
                    if(j.month == i)
                    {
                        array[count] = j.count;
                    }
                }
                count++;
           }
            return array;
        }

    }
}
