using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Domain.Entities;
using Personal.Shopping.Services.ShoppingCart.Infra.Context;

namespace Personal.Shopping.Services.ShoppingCart.Infra.Repositories;

public class CartHeaderRepository : ICartHeaderRepository
{
    private readonly AppDbContext _db;

    public CartHeaderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateCartHeader(CartHeader cartHeader)
    {
        _db.CartHeaders.Add(cartHeader);
        await _db.SaveChangesAsync();
    }

    public async Task<CartHeader> GetCartHeaderById(int cartHeaderId)
    {
        var cartHeader = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
            c => c.CartHeaderId == cartHeaderId);
        return cartHeader!;
    }

    public async Task<CartHeader> GetCartHeaderByUserId(string userId)
    {
        var cartHeader = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        return cartHeader!;
    }

    public async Task RemoveCartHeader(CartHeader cartHeader)
    {
        _db.CartHeaders.Remove(cartHeader);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateCartHeader(CartHeader cartHeader)
    {
        _db.CartHeaders.Update(cartHeader);
        await _db.SaveChangesAsync();
    }

}
