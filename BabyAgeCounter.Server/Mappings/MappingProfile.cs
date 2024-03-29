﻿using AutoMapper;
using BabyAgeCounter.Server.DTOs;
using BabyAgeCounter.Server.models;
using BabyAgeCounter.Server.utilities;

namespace BabyAgeCounter.Server.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateBabyMapper();
    }

    private void CreateBabyMapper()
    {
        CreateMap<BabyEntity, BabyDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTimeConverter.ToUtcMillis(src.Age)))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => DateTimeConverter.ToUtcMillis(src.DueDate)));

        CreateMap<BabyDto, BabyEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id!.ToString())))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => new DateTime(src.Age)))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => new DateTime(src.DueDate)));
    }
}