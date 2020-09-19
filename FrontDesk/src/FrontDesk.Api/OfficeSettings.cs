using FrontDesk.Core.Interfaces;
using System;

namespace FrontDesk.Api
{
    public class OfficeSettings : IApplicationSettings
    {
        public int ClinicId { get { return 1; } }
        public DateTime TestDate { get { return new DateTime(2014, 6, 9); } }
    }
}
