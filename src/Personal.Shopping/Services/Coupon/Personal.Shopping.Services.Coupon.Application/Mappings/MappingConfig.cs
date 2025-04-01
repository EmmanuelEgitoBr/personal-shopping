using AutoMapper;
using Personal.Shopping.Services.Coupon.Application.Dtos;
using Entities = Personal.Shopping.Services.Coupon.Domain.Entities;

namespace Personal.Shopping.Services.Coupon.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<Entities.Coupon, CouponDto>().ReverseMap();
            }
        );
        return mapperConfiguration;
    }
}
