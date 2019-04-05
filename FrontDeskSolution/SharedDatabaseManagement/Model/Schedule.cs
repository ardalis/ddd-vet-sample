using System;
using System.Collections.Generic;
using System.Linq;

namespace VetOffice.SharedDatabase.Model
{
    public class Schedule 
    {
        public Guid Id { get; set; }
        public int ClinicId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}