using Personal.Shopping.Services.ShoppingCart.Domain.Entities;

namespace Personal.Shopping.Services.ShoppingCart.Domain.Contracts;

public interface ICartHeaderRepository
{
    Task CreateCartHeader(CartHeader cartHeader);
    Task<CartHeader> GetCartHeaderById(int cartHeaderId);
    Task<CartHeader> GetCartHeaderByUserId(string userId);
    Task RemoveCartHeader(CartHeader cartHeader);
    Task UpdateCartHeader(CartHeader cartHeader);
}
