using AutoMapper;
using BlazorShared.Models.Appointment;
using BlazorShared.Models.AppointmentType;
using BlazorShared.Models.Client;
using BlazorShared.Models.Doctor;
using BlazorShared.Models.Patient;
using BlazorShared.Models.Room;
using BlazorShared.Models.Schedule;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<CreateScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
            CreateMap<DeleteScheduleRequest, Schedule>();

            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<CreateAppointmentRequest, Appointment>();
            CreateMap<UpdateAppointmentRequest, Appointment>();
            CreateMap<DeleteAppointmentRequest, Appointment>();

            CreateMap<AppointmentType, AppointmentTypeDto>();
            CreateMap<AppointmentTypeDto, AppointmentType>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>();
            CreateMap<DeleteClientRequest, Client>();

            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<CreateDoctorRequest, Doctor>();
            CreateMap<UpdateDoctorRequest, Doctor>();
            CreateMap<DeleteDoctorRequest, Doctor>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();
            CreateMap<DeleteRoomRequest, Room>();

            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<CreatePatientRequest, Patient>();
            CreateMap<UpdatePatientRequest, Patient>();
            CreateMap<DeletePatientRequest, Patient>();
        }
    }
}
