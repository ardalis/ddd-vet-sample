using AutoMapper;
using BlazorShared.Models.Appointment;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
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
        }
    }
}
