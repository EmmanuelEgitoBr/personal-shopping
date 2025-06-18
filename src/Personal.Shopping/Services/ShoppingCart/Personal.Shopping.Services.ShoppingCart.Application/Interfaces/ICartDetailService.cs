using Personal.Shopping.Services.ShoppingCart.Application.Dtos;

namespace Personal.Shopping.Services.ShoppingCart.Application.Interfaces;

public interface ICartDetailService
{
    Task CreateCartDeatilAsync(CartDetailDto cartDetailDto);
    Task<CartDetailDto> GetCartDetailByIdAsync(int cartDetailId);
    Task<CartDetailDto> GetCartDetailByProductIdAsync(IEnumerable<CartDetailDto> cartDetailsDto, int cardHeaderId);
    IEnumerable<CartDetailDto> GetCartDetailsByCartHeaderId(int cartHeaderId);
    int GetTotalItemsCountByCartHeaderId(int cartHeaderId);
    Task RemoveCartDetailAsync(CartDetailDto cartDetailDto);
    Task UpdateCartDetailsAsync(CartDetailDto cartDetailDto);
}
