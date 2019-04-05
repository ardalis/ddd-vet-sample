using FrontDesk.SharedKernel;

namespace AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Room : Entity<int>
    {
        public virtual string Name { get; set; }

        public Room(int id)
            : base(id)
        {
        }
    }
}
