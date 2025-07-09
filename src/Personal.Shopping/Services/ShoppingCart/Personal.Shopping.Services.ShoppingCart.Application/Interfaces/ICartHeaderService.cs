using Personal.Shopping.Services.ShoppingCart.Application.Dtos;

namespace Personal.Shopping.Services.ShoppingCart.Application.Interfaces;

public interface ICartHeaderService
{
    Task<int> CreateCartHeaderAsync(CartHeaderDto cartHeaderDto);
    Task<CartHeaderDto> GetCartHeaderByIdAsync(int cartHeaderId);
    Task<CartHeaderDto> GetCartHeaderByUserIdAsync(string userId);
    Task RemoveCartHeaderAsync(CartHeaderDto cartHeaderDto);
    Task UpdateCartHeaderAsync(CartHeaderDto cartHeaderDto);
}
