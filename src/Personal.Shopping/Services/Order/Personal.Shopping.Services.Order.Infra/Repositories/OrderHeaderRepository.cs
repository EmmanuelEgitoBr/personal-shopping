using Microsoft.EntityFrameworkCore;
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

    public async Task<OrderHeader> GetOrderHeaderByIdAsync(int orderHeaderId)
    {
        return await _db.OrderHeaders.AsNoTracking().FirstAsync(o => o.OrderHeaderId == orderHeaderId);
    }

    public async Task<OrderHeader> CreateCartHeader(OrderHeader orderHeader)
    {
        _db.OrderHeaders.Add(orderHeader);
        await _db.SaveChangesAsync();
        return orderHeader;
    }

    public async Task UpdateOrderHeaderAsync(OrderHeader orderHeader)
    {
        _db.OrderHeaders.Update(orderHeader);
        await _db.SaveChangesAsync();
    }
}
