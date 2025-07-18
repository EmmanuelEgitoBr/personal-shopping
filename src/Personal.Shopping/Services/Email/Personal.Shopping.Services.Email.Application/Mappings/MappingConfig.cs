using AutoMapper;
using Personal.Shopping.Services.Email.Application.Dtos;
using Personal.Shopping.Services.Email.Domain.Entities;

namespace Personal.Shopping.Services.Email.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<EmailLogger, EmailLoggerDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
