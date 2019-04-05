using FrontDesk.SharedKernel.Enums;
using VetOffice.SharedDatabase.Model.ValueObjects;

namespace VetOffice.SharedDatabase.Model
{
  public class Patient
  {
    public int Id { get; set; }
    public Client Owner { get; private set; }
    public int ClientId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public AnimalType AnimalType { get;  set; }
    public int? PreferredDoctorId { get; set; }

    public Patient(Client owner)
      : this()
    {
      ClientId = owner.Id;
      Owner = owner;
      PreferredDoctorId = Owner.PreferredDoctorId;
    }

    public Patient()
    {
    }
  }
}