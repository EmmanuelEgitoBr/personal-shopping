using Personal.Shopping.Services.ShoppingCart.Domain.Entities;

namespace Personal.Shopping.Services.ShoppingCart.Domain.Contracts;

public interface ICartDetailRepository
{
    Task CreateCartDeatil(CartDetail cartDetail);
    Task<CartDetail> GetCartDetailById(int cartDetailId);
    Task<CartDetail> GetCartDetailByProductId(int productId, int cardHeaderId);
    IEnumerable<CartDetail> GetCartDetailsByCartHeaderId(int cartHeaderId);
    int GetTotalItemsCountByCartHeaderId(int cartHeaderId);
    Task RemoveCartDetail(CartDetail cartDetail);
    Task UpdateCartDetails(CartDetail cartDetail);
}
