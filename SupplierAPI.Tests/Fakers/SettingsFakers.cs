using AutoMapper;

namespace SupplierAPI.Tests.Fakers;

public static class SettingsFakers
{
    public static IMapper GetMapper()
    {
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        return mappingConfig.CreateMapper();
    }
}