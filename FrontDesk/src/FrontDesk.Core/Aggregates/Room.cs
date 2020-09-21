using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Interfaces;

namespace FrontDesk.Core.Aggregates
{
    public class Room : BaseEntity<int>, IAggregateRoot
    {
        public virtual string Name { get; private set; }

        public Room(string name)
        {
            Name = name;
        }

        public Room(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        private Room() // required for EF
        {
        }
    }
}
