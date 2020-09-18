using FrontDesk.Core.ValueObjects;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;
using FrontDesk.SharedKernel.Interfaces;

namespace FrontDesk.Core.Aggregates
{
    public class Patient : BaseEntity<int>, IAggregateRoot
    {
        public int ClientId { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public virtual AnimalType AnimalType { get; private set; }
        public int? PreferredDoctorId { get; private set; }

        public Patient(int clientId, string name, Gender gender, AnimalType animalType, int? preferredDoctorId=null)
        {
            ClientId = clientId;
            Name = name;
            Gender = gender;
            AnimalType = animalType;
            PreferredDoctorId = preferredDoctorId;
        }

        public Patient(int id)
        {
            Id = id;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        private Patient() // required for EF
        {

        }
    }
}