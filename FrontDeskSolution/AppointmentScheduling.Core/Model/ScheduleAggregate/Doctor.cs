using FrontDesk.SharedKernel;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Doctor : Entity<int>
    {
        public virtual string Name { get; set; }

        public Doctor(int id)
            : base(id)
        {
        }

        private Doctor()
        {

        }
    }
}
