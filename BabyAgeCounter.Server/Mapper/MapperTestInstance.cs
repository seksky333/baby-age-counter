using AutoMapper;

namespace BabyAgeCounter.Server.Mapper;

public static class MapperTestInstance
{
    public static IMapper GetTestMapper() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        return config.CreateMapper();
    }
}