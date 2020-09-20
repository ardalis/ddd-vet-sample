﻿using AutoMapper;
using BlazorShared.Models.Patient;
using FrontDesk.Core.Aggregates;

namespace FrontDesk.Api.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dto => dto.PatientId, options => options.MapFrom(src => src.Id));
            CreateMap<PatientDto, Patient>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.PatientId));
            CreateMap<CreatePatientRequest, Patient>();
            CreateMap<UpdatePatientRequest, Patient>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.PatientId));
            CreateMap<DeletePatientRequest, Patient>();
        }
    }
}
