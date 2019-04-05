using System.Collections.Generic;
using FrontDesk.SharedKernel;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Client : Entity<int>
    {
        public string FullName { get; private set; }
        public IList<Patient> Patients { get; private set; }

        public Client(int id)
        {
            this.Id = id;
            Patients = new List<Patient>();
        }

        protected Client() //required for EF
        {
        }
    }
}