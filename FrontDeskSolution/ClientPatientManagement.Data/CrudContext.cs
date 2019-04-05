using System.Data.Entity;
using ClientPatientManagement.Core.Model;
using FrontDesk.SharedKernel.Enums;

namespace ClientPatientManagement.Data
{
    public class CrudContext : DbContext
    {
     
     
        public CrudContext()
        : base("name=VetOfficeContext")
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }

    public class TestInitializerForCrudContext : DropCreateDatabaseAlways<CrudContext>
    {
        protected override void Seed(CrudContext context)
        {
            base.Seed(context);

            // Add Doctors
            var drSmith = new Doctor { Name = "Dr. Smith" };
            var drWho = new Doctor { Name = "Dr. Who" };
            var drMcDreamy = new Doctor { Name = "Dr. McDreamy" };
            context.Doctors.Add(drSmith);
            context.Doctors.Add(drWho);
            context.Doctors.Add(drMcDreamy);

            var clientSteve = new Client
            {
                FullName = "Steve Smith",
                PreferredName = "Steve",
                Salutation = "Mr.",
                PreferredDoctorId = drSmith.Id
            };
            context.Clients.Add(clientSteve);
            context.Patients.Add(new Patient(clientSteve) {Gender = Gender.Male, Name = "Darwin"});
            context.Patients.Add(new Patient(clientSteve)
            {
                Gender = Gender.Female,
                Name = "Rumor",
                PreferredDoctorId = drWho.Id
            });

            var clientJulie = new Client
            {
                FullName = "Julia Lerman",
                PreferredName = "Julie",
                Salutation = "Mrs.",
                PreferredDoctorId = drMcDreamy.Id
            };
            context.Clients.Add(clientJulie);
            context.Patients.Add(new Patient(clientJulie) {Gender = Gender.Male, Name = "Sampson"});

            // add Rooms
            for (int i = 0; i < 5; i++)
            {
                var room = new Room { Name = string.Format("Exam Room {0}", i+1) };
                context.Rooms.Add(room);
            }
        }
    }
}