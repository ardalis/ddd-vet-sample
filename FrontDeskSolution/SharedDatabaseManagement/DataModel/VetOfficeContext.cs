using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;
using VetOffice.SharedDatabase.Model;
using VetOffice.SharedDatabase.Model.ValueObjects;

namespace VetOffice.SharedDatabase.DataModel
{
  public class VetOfficeContext : DbContext
  {
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<AppointmentType> AppointmentTypes { get; set; }
  }

  public class TestInitializerForVetContext : DropCreateDatabaseAlways<VetOfficeContext>
  {
    protected override void Seed(VetOfficeContext context)
    {
      // Add Schedule
      var schedule = new Schedule
      {
        Id = Guid.NewGuid(),
        ClinicId = 1
      };
      context.Schedules.Add(schedule);

      // Add Doctors
      var drSmith = new Doctor {Name = "Dr. Smith"};
      var drWho = new Doctor {Name = "Dr. Who"};
      var drMcDreamy = new Doctor {Name = "Dr. McDreamy"};
      context.Doctors.Add(drSmith);
      context.Doctors.Add(drWho);
      context.Doctors.Add(drMcDreamy);
      context.SaveChanges();

   
      context.Clients.AddRange(CreateListOfClientsWithPatients(drSmith, drWho,drMcDreamy));
     
      
     
      

      

   

      
     
      // add Rooms
      for (int i = 0; i < 5; i++)
      {
        var room = new Room {Name = string.Format("Exam Room {0}", i + 1)};
        context.Rooms.Add(room);
      }

      context.AppointmentTypes.Add(new AppointmentType
      {
        Code = "WE",
        Name = "Wellness Exam",
        Duration = 30
      });
      context.AppointmentTypes.Add(new AppointmentType
      {
        Code = "DE",
        Name = "Diagnostic Exam",
        Duration = 60
      });
      context.AppointmentTypes.Add(new AppointmentType
      {
        Code = "NT",
        Name = "Nail Trim",
        Duration = 30
      });

      // add Appointments
      context.Appointments.AddRange(GetAppointments(schedule.Id));
      base.Seed(context);
    }

    private static IEnumerable<Client> CreateListOfClientsWithPatients(Doctor drSmith, Doctor drWho, Doctor drMcDreamy)
    {
      var clientGraphs = new List<Client>();
      var clientSmith=(CreateClientWithPatient("Steve Smith", "Steve", "Mr.", drSmith.Id, Gender.Male, "Darwin", "Dog",
        "Poodle"));
      clientSmith.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Rumor",
        PreferredDoctorId = drWho.Id,
        AnimalType = new AnimalType("Cat", "Alley")
      });
      clientGraphs.Add(clientSmith);

      clientGraphs.Add(CreateClientWithPatient("Julia Lerman", "Julie", "Mrs.", drMcDreamy.Id, Gender.Male, "Sampson", "Dog","Newfoundland"));
      clientGraphs.Add(CreateClientWithPatient("Wes McClure", "Wes", "Mr", drMcDreamy.Id, Gender.Female, "Pax", "Dog",
        "Jack Russell"));
      clientGraphs.Add(CreateClientWithPatient("Andrew Mallett", "Andrew", "Mr.", drSmith.Id, Gender.Male, "Barney",
        "Dog", "Corgi"));
      clientGraphs.Add(CreateClientWithPatient("Brian Lagunas", "Brian", "Mr.", drWho.Id, Gender.Male, "Rocky", "Dog",
        "Italian Greyhound"));
      clientGraphs.Add(CreateClientWithPatient("Corey Haines", "Corey", "Mr.", drMcDreamy.Id, Gender.Female, "Zak",
        "Cat", "Mixed"));
      clientGraphs.Add(CreateClientWithPatient("Reindert-Jan Ekker", "Reindert", "Mr.", drSmith.Id, Gender.Female,
        "Tinkelbel", "Cat", "Mixed"));
      clientGraphs.Add(CreateClientWithPatient("Patrick Hynds", "Patrick", "Mr.", drMcDreamy.Id, Gender.Male, "Anubis", "Dog",
          "Doberman"));
      clientGraphs.Add(CreateClientWithPatient("Lars Klint", "Lars", "Mr.", drMcDreamy.Id, Gender.Male, "Boots", "Cat",
        "Tabby"));
      clientGraphs.Add(CreateClientWithPatient("Joe Eames", "Joe", "Mr.", drMcDreamy.Id, Gender.Male, "Corde", "Dog",
        "Mastiff"));
      clientGraphs.Add(CreateClientWithPatient("Joe Kunk", "Joe", "Mr.", drSmith.Id, Gender.Male, "Wedgie","Reptile",
        "Python"));
      clientGraphs.Add(CreateClientWithPatient("Ross Bagurdes", "Ross", "Mr.", drWho.Id, Gender.Male, "Indiana Jones",
        "Cat", "Tabby"));
      clientGraphs.Add(CreateClientWithPatient("Patrick Neborg", "Patrick", "Mr.", drWho.Id, Gender.Female, "Sugar",
        "Dog", "Maltese"));
      clientGraphs.Add(CreateClientWithPatient("David Mann", "David", "Mr.", drWho.Id, Gender.Female, "Piper",
     "Dog", "Mix"));
      clientGraphs.Add(CreateClientWithPatient("Peter Mourfield", "Peter", "Mr.", drWho.Id, Gender.Female, "Finley",
   "Dog", "Dachshund"));
      clientGraphs.Add(CreateClientWithPatient("Keith Harvey", "Keith", "Mr.", drSmith.Id, Gender.Female, "Bella",
 "Dog", "Terrier"));
      var clientMcConnell=CreateClientWithPatient("Andrew Connell","Andrew","Mr.",drWho.Id,Gender.Female,"Luabelle","Dog","Labrador");
       clientMcConnell.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Sadie",
        AnimalType = new AnimalType("Dog", "Mix"),
        PreferredDoctorId = drWho.Id
      });
      clientGraphs.Add(clientMcConnell);
      var clientYack = (CreateClientWithPatient("Julie Yack", "Julie", "Ms.", drSmith.Id, Gender.Male, "Ruske", "Dog",
       "Siberian Husky"));
      clientYack.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Ginger",
        AnimalType = new AnimalType("Dog", "Shih Tzu"),
        PreferredDoctorId = drSmith.Id
      });
      clientYack.Patients.Add(new Patient{
        Gender = Gender.Male,
        Name = "Lizzie",
        AnimalType = new AnimalType("Lizard", "Green"),
        PreferredDoctorId = drSmith.Id
      });
      clientGraphs.Add(clientYack);

      var clientLibery =
        CreateClientWithPatient("Jesse Liberty", "Jesse", "Mr.", drMcDreamy.Id, Gender.Male, "Charlie", "Dog", "Mixed");
      clientLibery.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Allegra",
        AnimalType = new AnimalType("Cat", "Calico"),
        PreferredDoctorId = drSmith.Id

      });
      clientLibery.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Misty",
        AnimalType = new AnimalType("Cat", "Tortoiseshell"),
        PreferredDoctorId = drSmith.Id

      });
      clientGraphs.Add(clientLibery);
      var clientYoung =CreateClientWithPatient("Tyler Young","Tyler","Mr.",drMcDreamy.Id,Gender.Male,"Willie","Dog", "Daschaund");
      clientYoung.Patients.Add(new Patient{
        Gender = Gender.Male,
        Name = "JoeFish",
        AnimalType = new AnimalType("Fish", "Beta"),
        PreferredDoctorId = drSmith.Id

      });
      clientYoung.Patients.Add(new Patient{
        Gender = Gender.Male,
        Name = "Fabian",
        AnimalType = new AnimalType("Cat", "Mixed"),
        PreferredDoctorId = drMcDreamy.Id
      });
      clientGraphs.Add(clientYoung);
      var clientPerry =
        (CreateClientWithPatient("Michael Perry", "Michael", "Mr.", drMcDreamy.Id, Gender.Female, "Callie", "Cat",
          "Calico"));

      clientPerry.Patients.Add(new Patient{
        Gender = Gender.Male,
        Name = "Radar",
        AnimalType = new AnimalType("Dog", "Pug"),
        PreferredDoctorId = drMcDreamy.Id
      });
      clientPerry.Patients.Add(new Patient{
        Gender = Gender.Female,
        Name = "Tinkerbell",
        AnimalType = new AnimalType("Dog", "Chihuahua"),
        PreferredDoctorId = drMcDreamy.Id
      });
      clientGraphs.Add(clientPerry);
      return clientGraphs;

    }

    private static Client CreateClientWithPatient(string fullName, string preferredName, string salutation,
      int doctorId, Gender patient1Gender, string patient1Name, string animalType,string breed)
    {
      var client = new Client
      {
        FullName = fullName,
        PreferredName = preferredName,
        Salutation = salutation,
        PreferredDoctorId = doctorId,
        EmailAddress = "client@example.com"
      };
      client.Patients.Add(new Patient
      {
        Gender = patient1Gender,
        Name = patient1Name,
        AnimalType = new AnimalType(animalType,breed),
        PreferredDoctorId = doctorId
      });
      return client;
    }

    private static IEnumerable<Appointment> GetAppointments(Guid scheduleId)
    {
      var appointmentList = new List<Appointment>
      {
        new Appointment
        {
          AppointmentTypeId = 1,
          ScheduleId = scheduleId,
          ClientId = 1,
          DoctorId = 1,
          Id = Guid.NewGuid(),
          PatientId = 1,
          RoomId = 1,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 10, 0, 0),
            new DateTime(2014, 6, 9, 11, 0, 0)),
          Title = "(WE) Darwin - Steve Smith"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 2,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 3,
          RoomId = 2,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 11, 0, 0),
            new DateTime(2014, 6, 9, 11, 30, 0))
          ,
          Title = "(DE) Sampson - Julie Lerman"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 3,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 4,
          RoomId = 2,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 12, 0, 0),
            new DateTime(2014, 6, 9, 12, 30, 0))
          ,
          Title = "(DE) Pax - Wes McClure"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 19,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 23,
          RoomId = 3,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Charlie - Jesse Liberty"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 19,
          DoctorId = 6,
          Id = Guid.NewGuid(),
          PatientId = 24,
          RoomId = 3,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 30, 0),
            new DateTime(2014, 6, 9, 10, 30, 0))
          ,
          Title = "(DE) Allegra - Jesse Liberty"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 19,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 25,
          RoomId = 3,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 10, 30, 0),
            new DateTime(2014, 6, 9, 11, 00, 0))
          ,
          Title = "(DE) Misty - Jesse Liberty"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 4,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 5,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 8, 0, 0),
            new DateTime(2014, 6, 9, 8, 30, 0))
          ,
          Title = "(DE) Barney - Andrew Mallett"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 5,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 6,
          RoomId = 3,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 8, 0, 0),
            new DateTime(2014, 6, 9, 9, 0, 0))
          ,
          DateTimeConfirmed=new DateTime(2014,6,8,8,0,0),
          Title = "(DE) Rocky - Brian Lagunas"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 20,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 26,
          RoomId = 2,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Willie - Tyler Young"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 20,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 27,
          RoomId = 2,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 30, 0),
            new DateTime(2014, 6, 9, 10, 00, 0))
          ,
          Title = "(DE) JoeFish - Tyler Young"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 20,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 28,
          RoomId = 2,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 10, 0, 0),
            new DateTime(2014, 6, 9, 10, 30, 0))
          ,
          Title = "(DE) Fabian - Tyler Young"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 6,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 7,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 11, 0, 0),
            new DateTime(2014, 6, 9, 11, 30, 0))
          ,
          Title = "(DE) Zak - Corey Haines"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 7,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 8,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Tinkelbel - Reindert Ekkert"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 18,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 20,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Ruske - Julie Yack"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 18,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 22,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 30, 0),
            new DateTime(2014, 6, 9, 10, 00, 0))
          ,
          Title = "(DE) Lizzie - Julie Yack"
        },
        new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 18,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 21,
          RoomId = 4,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 10, 0, 0),
            new DateTime(2014, 6, 9, 10, 30, 0))
          ,
          Title = "(DE) Ginger - Julie Yack"
        }
      ,
          new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 8,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 9,
          RoomId = 5,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 8, 0, 0),
            new DateTime(2014, 6, 9, 9, 00, 0))
          ,
          Title = "(DE) Anubis - Patrick Hynds"
        },
          new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 21,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 30,
          RoomId = 1,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Radar - Michael Perry"
        },
          new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 21,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 31,
          RoomId = 1,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Tinkerbell - Michael Perry"
        },
          new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 10,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 11,
          RoomId = 1,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 8, 0, 0),
            new DateTime(2014, 6, 9, 9, 00, 0))
          ,
          Title = "(DE) Corde - Joe Eames"
        },
          new Appointment
        {
          AppointmentTypeId = 2,
          ScheduleId = scheduleId,
          ClientId = 21,
          DoctorId = 2,
          Id = Guid.NewGuid(),
          PatientId = 29,
          RoomId = 5,
          TimeRange = new DateTimeRange(
            new DateTime(2014, 6, 9, 9, 0, 0),
            new DateTime(2014, 6, 9, 9, 30, 0))
          ,
          Title = "(DE) Callie - Michael Perry"
        }
      };

      return appointmentList;
    }
  }
}