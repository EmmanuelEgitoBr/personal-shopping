using AutoMapper;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Dtos.Product;
using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.ValueObjects;

namespace Personal.Shopping.Services.Order.Application.Mappings;

public class MappingConfig
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            // CartHeaderDto -> OrderHeaderDto
            config.CreateMap<CartHeaderDto, OrderHeaderDto>()
                .ForMember(dest => dest.OrderHeaderId, opt => opt.Ignore()) // se não quiser mapear ID
                .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.CartTotal))
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // se não existir no Cart

            // OrderHeaderDto -> CartHeaderDto
            config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, opt => opt.MapFrom(src => src.OrderTotal))
                .ForMember(dest => dest.CartHeaderId, opt => opt.Ignore());

            config.CreateMap<CartDetailDto, OrderDetailDto>()
            .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));
 
            // OrderHeader ↔ OrderHeaderDto
            config.CreateMap<OrderHeader, OrderHeaderDto>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            config.CreateMap<OrderHeaderDto, OrderHeader>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            // OrderDetail ↔ OrderDetailDto
            config.CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

            // Se tiver Product/ProductDto
            config.CreateMap<Product, ProductDto>().ReverseMap();

            config.CreateMap<OrderLog, OrderLogDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
