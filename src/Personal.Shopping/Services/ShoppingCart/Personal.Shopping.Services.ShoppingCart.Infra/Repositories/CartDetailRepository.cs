using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Infra.Context;

namespace Personal.Shopping.Services.ShoppingCart.Infra.Repositories;

public class CartDetailRepository : ICartDetailRepository
{
    private readonly AppDbContext _db;
    public CartDetailRepository(AppDbContext db)
    {
        _db = db;
    }


}
