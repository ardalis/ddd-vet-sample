using FrontDesk.Core.Aggregates;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Enums;
using FrontDesk.Core.ValueObjects;

namespace FrontDesk.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private static Doctor DrSmith => new Doctor("Dr. Smith");
        private static Doctor DrWho => new Doctor("Dr. Who");
        private static Doctor DrMcDreamy => new Doctor("Dr. McDreamy");
        private static Guid ScheduleId;
        private static DateTime TestDate;

        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, DateTime TestDate, int? retry = 0)
        {
            AppDbContextSeed.TestDate = TestDate;
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!await context.Schedules.AnyAsync())
                {
                    await context.Schedules.AddAsync(
                        CreateSchedule());

                    await context.SaveChangesAsync();
                }

                if (!await context.AppointmentTypes.AnyAsync())
                {
                    await context.AppointmentTypes.AddRangeAsync(
                        CreateAppointmentTypes());

                    await context.SaveChangesAsync();
                }

                if (!await context.Doctors.AnyAsync())
                {
                    await context.Doctors.AddRangeAsync(
                        CreateDoctors());

                    await context.SaveChangesAsync();
                }

                if (!await context.Clients.AnyAsync())
                {
                    await context.Clients.AddRangeAsync(
                        CreateListOfClientsWithPatients(DrSmith, DrWho, DrMcDreamy));

                    await context.SaveChangesAsync();
                }

                if (!await context.Rooms.AnyAsync())
                {
                    await context.Rooms.AddRangeAsync(
                        CreateRooms());

                    await context.SaveChangesAsync();
                }

                if (!await context.Appointments.AnyAsync())
                {
                    await context.Appointments.AddRangeAsync(
                        CreateAppointments(ScheduleId));

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AppDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, TestDate, retryForAvailability);
                }
                throw;
            }

            context.SaveChanges();           
        }

        private static List<Room> CreateRooms()
        {
            var result = new List<Room>();

            for (int i = 0; i < 5; i++)
            {
                var room = new Room(string.Format("Exam Room {0}", i + 1));
                result.Add(room);
            }

            return result;
        }

        private static Schedule CreateSchedule()
        {
            ScheduleId = Guid.NewGuid();
            return new Schedule(ScheduleId, new DateTimeRange(DateTime.Now, DateTime.Now), 1, null);
        }

        private static List<AppointmentType> CreateAppointmentTypes()
        {
            var result = new List<AppointmentType>();

            result.Add(new AppointmentType("Wellness Exam", "WE", 30));
            result.Add(new AppointmentType("Diagnostic Exam", "DE", 60));
            result.Add(new AppointmentType("Nail Trim", "NT", 30));

            return result;
        }

        private static List<Doctor> CreateDoctors()
        {
            var result = new List<Doctor>();            

            result.Add(DrSmith);
            result.Add(DrWho);
            result.Add(DrMcDreamy);

            return result;
        }

        private static IEnumerable<Client> CreateListOfClientsWithPatients(Doctor drSmith, Doctor drWho, Doctor drMcDreamy)
        {
            var clientGraphs = new List<Client>();
            var clientSmith = (CreateClientWithPatient("Steve Smith", "Steve", "Mr.", drSmith.Id, Gender.Male, "Darwin", "Dog",
              "Poodle"));
            clientSmith.Patients.Add(new Patient(1, "Rumor", Gender.Female, new AnimalType("Cat", "Alley"), drWho.Id));

            clientGraphs.Add(clientSmith);

            clientGraphs.Add(CreateClientWithPatient("Julia Lerman", "Julie", "Mrs.", drMcDreamy.Id, Gender.Male, "Sampson", "Dog", "Newfoundland"));
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
            clientGraphs.Add(CreateClientWithPatient("Joe Kunk", "Joe", "Mr.", drSmith.Id, Gender.Male, "Wedgie", "Reptile",
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
            var clientMcConnell = CreateClientWithPatient("Andrew Connell", "Andrew", "Mr.", drWho.Id, Gender.Female, "Luabelle", "Dog", "Labrador");
            clientMcConnell.Patients.Add(new Patient(1, "Sadie", Gender.Female, new AnimalType("Dog", "Mix"), drWho.Id));
            clientGraphs.Add(clientMcConnell);
            var clientYack = (CreateClientWithPatient("Julie Yack", "Julie", "Ms.", drSmith.Id, Gender.Male, "Ruske", "Dog",
             "Siberian Husky"));
            clientYack.Patients.Add(new Patient(1, "Ginger", Gender.Female, new AnimalType("Dog", "Shih Tzu"), drSmith.Id));
            clientYack.Patients.Add(new Patient(1, "Lizzie", Gender.Male, new AnimalType("Lizzie", "Green"), drSmith.Id));
            clientGraphs.Add(clientYack);

            var clientLibery =
              CreateClientWithPatient("Jesse Liberty", "Jesse", "Mr.", drMcDreamy.Id, Gender.Male, "Charlie", "Dog", "Mixed");
            clientLibery.Patients.Add(new Patient(1, "Allegra", Gender.Female, new AnimalType("Cat", "Calico"), drSmith.Id));
            clientLibery.Patients.Add(new Patient(1, "Misty", Gender.Female, new AnimalType("Cat", "Tortoiseshell"), drSmith.Id));
            clientGraphs.Add(clientLibery);
            var clientYoung = CreateClientWithPatient("Tyler Young", "Tyler", "Mr.", drMcDreamy.Id, Gender.Male, "Willie", "Dog", "Daschaund");
            clientLibery.Patients.Add(new Patient(1, "JoeFish", Gender.Male, new AnimalType("Fish", "Beta"), drSmith.Id));
            clientLibery.Patients.Add(new Patient(1, "Fabian", Gender.Male, new AnimalType("Cat", "Mixed"), drMcDreamy.Id));
            clientGraphs.Add(clientYoung);
            var clientPerry =
              (CreateClientWithPatient("Michael Perry", "Michael", "Mr.", drMcDreamy.Id, Gender.Female, "Callie", "Cat",
                "Calico"));

            clientLibery.Patients.Add(new Patient(1, "Radar", Gender.Male, new AnimalType("Dog", "Pug"), drMcDreamy.Id));
            clientLibery.Patients.Add(new Patient(1, "Tinkerbell", Gender.Female, new AnimalType("Dog", "Chihuahua"), drMcDreamy.Id));

            clientGraphs.Add(clientPerry);

            return clientGraphs;
        }

        private static Client CreateClientWithPatient(string fullName, string preferredName, string salutation, 
            int doctorId, Gender patient1Gender, string patient1Name, string animalType, string breed)
        {
            var client = new Client(fullName, preferredName, salutation, doctorId, "client@example.com");
            client.Patients.Add(new Patient(1, patient1Name, patient1Gender, new AnimalType(animalType, breed), doctorId));            

            return client;
        }

        private static IEnumerable<Appointment> CreateAppointments(Guid scheduleId)
        {
            var appointmentList = new List<Appointment>
              {
                new Appointment(
                    1, 
                    scheduleId, 
                    1, 
                    1, 
                    1, 
                    1, 
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 0, 0)), 
                    "(WE) Darwin - Steve Smith"),
                new Appointment(
                    2,
                    scheduleId,
                    2,
                    2,
                    3,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 30, 0)),
                    "(DE) Sampson - Julie Lerman"),
                new Appointment(
                    2,
                    scheduleId,
                    3,
                    2,
                    4,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 12, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 12, 30, 0)),
                    "(DE) Pax - Wes McClure"),
                new Appointment(
                    2,
                    scheduleId,
                    19,
                    2,
                    23,
                    3,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Charlie - Jesse Liberty"),
                new Appointment(
                    2,
                    scheduleId,
                    19,
                    6,
                    24,
                    3,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 30, 0)),
                    "(DE) Allegra - Jesse Liberty"),
                new Appointment(
                    2,
                    scheduleId,
                    19,
                    2,
                    25,
                    3,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 30, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 00, 0)),
                    "(DE) Misty - Jesse Liberty"),
                new Appointment(
                    2,
                    scheduleId,
                    4,
                    2,
                    5,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 8, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 8, 30, 0)),
                    "(DE) Barney - Andrew Mallett"),
                new Appointment(
                    2,
                    scheduleId,
                    5,
                    2,
                    6,
                    3,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 8, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0)),                    
                    "(DE) Rocky - Brian Lagunas",
                    new DateTime(2014,6,8,8,0,0)),
                new Appointment(
                    2,
                    scheduleId,
                    20,
                    2,
                    26,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Willie - Tyler Young"),
                new Appointment(
                    2,
                    scheduleId,
                    20,
                    2,
                    27,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 00, 0)),
                    "(DE) JoeFish - Tyler Young"),
                new Appointment(
                    2,
                    scheduleId,
                    20,
                    2,
                    27,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 00, 0)),
                    "(DE) JoeFish - Tyler Young"),
                new Appointment(
                    2,
                    scheduleId,
                    20,
                    2,
                    28,
                    2,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 30, 0)),
                    "(DE) Fabian - Tyler Young"),
                new Appointment(
                    2,
                    scheduleId,
                    6,
                    2,
                    7,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 11, 30, 0)),
                    "(DE) Zak - Corey Haines"),
                new Appointment(
                    2,
                    scheduleId,
                    7,
                    2,
                    8,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Tinkelbel - Reindert Ekkert"),
                new Appointment(
                    2,
                    scheduleId,
                    18,
                    2,
                    20,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Ruske - Julie Yack"),
                new Appointment(
                    2,
                    scheduleId,
                    18,
                    2,
                    22,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 00, 0)),
                    "(DE) Lizzie - Julie Yack"),
                new Appointment(
                    2,
                    scheduleId,
                    18,
                    2,
                    21,
                    4,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 10, 30, 0)),
                    "(DE) Ginger - Julie Yack"),
                new Appointment(
                    2,
                    scheduleId,
                    8,
                    2,
                    9,
                    5,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 8, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 00, 0)),
                    "(DE) Anubis - Patrick Hynds"),
                new Appointment(
                    2,
                    scheduleId,
                    21,
                    2,
                    30,
                    1,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Radar - Michael Perry"),
                new Appointment(
                    2,
                    scheduleId,
                    21,
                    2,
                    31,
                    1,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Tinkerbell - Michael Perry"),
                new Appointment(
                    2,
                    scheduleId,
                    10,
                    2,
                    11,
                    1,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 8, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 00, 0)),
                    "(DE) Corde - Joe Eames"),
                new Appointment(
                    2,
                    scheduleId,
                    21,
                    2,
                    29,
                    5,
                    new DateTimeRange(
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 0, 0),
                        new DateTime(TestDate.Year, TestDate.Month, TestDate.Day, 9, 30, 0)),
                    "(DE) Callie - Michael Perry"),
              };

            return appointmentList;
        }
    }
}