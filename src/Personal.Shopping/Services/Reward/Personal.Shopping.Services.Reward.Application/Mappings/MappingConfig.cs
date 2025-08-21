using AutoMapper;
using Personal.Shopping.Services.Reward.Application.Dtos;
using Personal.Shopping.Services.Reward.Domain.Entities;

namespace Personal.Shopping.Services.Reward.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Rewards, RewardsDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
