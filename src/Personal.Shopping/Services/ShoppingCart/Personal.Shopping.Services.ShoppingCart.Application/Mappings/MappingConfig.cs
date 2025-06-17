using AutoMapper;

namespace Personal.Shopping.Services.ShoppingCart.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            //config.CreateMap<Entity.Product, ProductDto>().ReverseMap();
            //config.CreateMap<Category, CategoryDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
