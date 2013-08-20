namespace FrontDesk.Core.Model
{
    public class Patient
    {
        public Client Owner { get; private set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }

        public Patient(Client owner)
        {
            this.Owner = owner;
        }
    }
}