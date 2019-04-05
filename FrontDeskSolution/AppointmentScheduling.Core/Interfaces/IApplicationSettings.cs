using System;

namespace AppointmentScheduling.Core.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTime TestDate { get; }
    }
}