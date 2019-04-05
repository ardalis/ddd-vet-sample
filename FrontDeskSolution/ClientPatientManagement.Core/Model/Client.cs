using System;
using System.Collections.Generic;
using ClientPatientManagement.Core.Interfaces;

namespace ClientPatientManagement.Core.Model
{
  public class Client : IEntity
  {
    public Client()
    {
      Patients = new List<Patient>();
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string Salutation { get; set; }
    public string PreferredName { get; set; }
    public int? PreferredDoctorId { get; set; }

    public IList<Patient> Patients { get; private set; }
  }
}