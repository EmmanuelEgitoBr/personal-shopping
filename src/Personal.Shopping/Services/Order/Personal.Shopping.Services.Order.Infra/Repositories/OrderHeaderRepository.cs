using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.Interfaces;
using Personal.Shopping.Services.Order.Infra.Context;

namespace Personal.Shopping.Services.Order.Infra.Repositories;

public class OrderHeaderRepository : IOrderHeaderRepository
{
    private readonly AppDbContext _db;

    public OrderHeaderRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<OrderHeader> CreateCartHeader(OrderHeader orderHeader)
    {
        _db.OrderHeaders.Add(orderHeader);
        await _db.SaveChangesAsync();
        return orderHeader;
    }
}
