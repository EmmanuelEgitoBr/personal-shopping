using Personal.Shopping.Services.Order.Domain.Interfaces;
using Personal.Shopping.Services.Order.Infra.Context;

namespace Personal.Shopping.Services.Order.Infra.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly AppDbContext _db;

    public OrderDetailRepository(AppDbContext db)
    {
        _db = db;
    }
}
