using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Interfaces;

namespace FrontDesk.Core.Aggregates
{
    public class Room : BaseEntity<int>, IAggregateRoot
    {
        public virtual string Name { get; private set; }
        public string Color { get; private set; }

        public Room(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public Room(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public Room UpdateName(string name)
        {
            Name = name;
            return this;
        }

        public Room UpdateColor(string color)
        {
            Color = color;
            return this;
        }

        private Room() // required for EF
        {
        }
    }
}
