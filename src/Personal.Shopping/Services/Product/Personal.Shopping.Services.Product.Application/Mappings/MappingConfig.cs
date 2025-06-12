using AutoMapper;
using Personal.Shopping.Services.Product.Application.Dtos;
using Personal.Shopping.Services.Product.Domain.Entities;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Entity.Product, ProductDto>().ReverseMap();
            config.CreateMap<Category, CategoryDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
