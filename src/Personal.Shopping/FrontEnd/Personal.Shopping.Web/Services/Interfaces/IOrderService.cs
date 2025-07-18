using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.ShoppingCart;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
}
