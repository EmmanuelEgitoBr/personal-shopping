using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Infra.Context;

namespace Personal.Shopping.Services.ShoppingCart.Infra.Repositories;

public class CartHeaderRepository : ICartHeaderRepository
{
    private readonly AppDbContext _db;

    public CartHeaderRepository(AppDbContext db)
    {
        _db = db;
    }


}
