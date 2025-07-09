using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Domain.Entities;
using Personal.Shopping.Services.ShoppingCart.Infra.Context;

namespace Personal.Shopping.Services.ShoppingCart.Infra.Repositories;

public class CartDetailRepository : ICartDetailRepository
{
    private readonly AppDbContext _db;
    public CartDetailRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateCartDeatil(CartDetail cartDetail)
    {
        _db.CartDetails.Add(cartDetail);
        await _db.SaveChangesAsync();
    }

    public async Task<CartDetail> GetCartDetailById(int cartDetailId)
    {
        var cartDetail = await _db.CartDetails.FirstOrDefaultAsync(
            c => c.CartDetailsId == cartDetailId);
        return cartDetail!;
    }

    public async Task<CartDetail> GetCartDetailByProductId(int productId, int cardHeaderId)
    {
        //cartDetails.First().ProductId
        var cartDetail = await _db.CartDetails.FirstOrDefaultAsync(
            c => c.ProductId == productId &&
            c.CartHeaderId == cardHeaderId);
        return cartDetail!;
    }

    public IEnumerable<CartDetail> GetCartDetailsByCartHeaderId(int cartHeaderId)
    {
        var cartDetails = _db.CartDetails.Where(c => c.CartHeaderId == cartHeaderId).ToList();
        return cartDetails;
    }

    public int GetTotalItemsCountByCartHeaderId(int cartHeaderId)
    {
        int count = _db.CartDetails.Where(
            c => c.CartHeaderId == cartHeaderId).Count();
        return count;
    }

    public async Task RemoveCartDetail(CartDetail cartDetail)
    {
        _db.CartDetails.Remove(cartDetail);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateCartDetails(CartDetail cartDetail)
    {
        var local = _db.CartDetails.Local
            .FirstOrDefault(e => e.CartDetailsId == cartDetail.CartDetailsId);

        if (local != null)
            _db.Entry(local).State = EntityState.Detached;
        
        _db.CartDetails.Update(cartDetail);
        await _db.SaveChangesAsync();
    }
}
