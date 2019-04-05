using AppointmentScheduling.Core.Model.ValueObjects;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Patient : Entity<int>
    {
      public int ClientId { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public AnimalType AnimalType { get; private set; }
        public int? PreferredDoctorId { get; set; }

        public Patient(int id)
            : base(id)
        {
        }

        private Patient()
        {

        }
    }
}