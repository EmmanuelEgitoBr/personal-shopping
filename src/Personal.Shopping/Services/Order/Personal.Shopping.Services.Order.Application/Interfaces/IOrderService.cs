using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;

namespace Personal.Shopping.Services.Order.Application.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
}
