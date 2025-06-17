using AutoMapper;
using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Personal.Shopping.Services.ShoppingCart.Domain.Entities;

namespace Personal.Shopping.Services.ShoppingCart.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Product, ProductDto>().ReverseMap();
            config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
            config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
