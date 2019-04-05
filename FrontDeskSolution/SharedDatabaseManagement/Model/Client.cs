using System.Collections.Generic;

namespace VetOffice.SharedDatabase.Model
{
    public class Client
    {
        public Client()
        {
            Patients = new List<Patient>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Salutation { get; set; }
        public string EmailAddress { get; set; }
        public string PreferredName { get; set; }
        public int? PreferredDoctorId { get; set; }

        public IList<Patient> Patients { get; private set; }
    }
}