using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StructureMap;
using System;
using System.Linq;

namespace FrontDesk.Web.Hubs
{
    public class ScheduleHub : Hub
    {
        public void UpdateSchedule(string s)
        {
            Clients.All.showAlert(s);
        }
    }
}