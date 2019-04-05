using AppointmentScheduling.Core.Interfaces;
using System;
using System.Linq;

namespace FrontDesk.Web.Models
{
    public class OfficeSettings : IApplicationSettings
    {
        public int ClinicId { get { return 1; } }

        public DateTime TestDate { get { return new DateTime(2014, 6, 9); } }
    }
}