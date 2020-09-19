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

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dto => dto.AppointmentId, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.Start, options => options.MapFrom(src => src.TimeRange.Start))
                .ForMember(dto => dto.End, options => options.MapFrom(src => src.TimeRange.End))
                .ForMember(dto => dto.IsAllDay, options => options.MapFrom(src => false))
                .ForMember(dto => dto.Description, options => options.MapFrom(src => "No Description"))
                .ForMember(dto => dto.IsConfirmed, options => options.MapFrom(src => src.DateTimeConfirmed.HasValue));
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<CreateAppointmentRequest, Appointment>();
            CreateMap<UpdateAppointmentRequest, Appointment>();
            CreateMap<DeleteAppointmentRequest, Appointment>();            

            CreateMap<AppointmentType, AppointmentTypeDto>()
                .ForMember(dto => dto.AppointmentTypeId, options => options.MapFrom(src => src.Id));
            CreateMap<AppointmentTypeDto, AppointmentType>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.AppointmentTypeId));


            CreateMap<Client, ClientDto>()
                .ForMember(dto => dto.ClientId, options => options.MapFrom(src => src.Id));
            CreateMap<ClientDto, Client>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.ClientId));
            CreateMap<CreateClientRequest, Client>();
            CreateMap<UpdateClientRequest, Client>();
            CreateMap<DeleteClientRequest, Client>();

            CreateMap<Doctor, DoctorDto>()
                .ForMember(dto => dto.DoctorId, options => options.MapFrom(src => src.Id));
            CreateMap<DoctorDto, Doctor>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.DoctorId));
            CreateMap<CreateDoctorRequest, Doctor>();
            CreateMap<UpdateDoctorRequest, Doctor>();
            CreateMap<DeleteDoctorRequest, Doctor>();

            CreateMap<Room, RoomDto>()
                .ForMember(dto => dto.RoomId, options => options.MapFrom(src => src.Id));
            CreateMap<RoomDto, Room>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.RoomId));
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();
            CreateMap<DeleteRoomRequest, Room>();

            CreateMap<Patient, PatientDto>()
                .ForMember(dto => dto.PatientId, options => options.MapFrom(src => src.Id));
            CreateMap<PatientDto, Patient>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.PatientId));
            CreateMap<CreatePatientRequest, Patient>();
            CreateMap<UpdatePatientRequest, Patient>();
            CreateMap<DeletePatientRequest, Patient>();
        }
    }
}
