using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.ShoppingCart;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IShoppingCartService
{
    Task<ResponseDto> CartUpsertAsync(CartDto cart);
    Task<ResponseDto> RemoveCartAsync(int id);
    Task<ResponseDto> GetCartByUserIdAsync(string userId);
    Task<ResponseDto> ApplyCouponAsync(CartDto cart);
    Task<ResponseDto> RemoveCouponAsync(CartDto cart);
    Task<ResponseDto> EmailCartAsync(CartDto cart);
}
