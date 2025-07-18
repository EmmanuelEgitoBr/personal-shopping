using AutoMapper;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<OrderHeaderDto, CartHeaderDto>()
            .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

            config.CreateMap<CartDetailDto, OrderDetailDto>()
            .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

            config.CreateMap<OrderDetailDto, CartDetailDto>();

            config.CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
