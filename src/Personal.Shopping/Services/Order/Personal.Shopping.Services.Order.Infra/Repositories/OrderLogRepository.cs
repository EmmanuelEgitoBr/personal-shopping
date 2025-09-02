using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Order.Domain.Entity;
using Personal.Shopping.Services.Order.Domain.Interfaces;
using Personal.Shopping.Services.Order.Infra.Context;

namespace Personal.Shopping.Services.Order.Infra.Repositories;

public class OrderLogRepository : IOrderLogRepository
{
    private readonly AppDbContext _db;

    public OrderLogRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<OrderLog>> GetAllOrdersAsync()
    {
        return await _db.OrderLogs
            .AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<OrderLog>> GetAllOrdersByCurrentDayAsync()
    {
        DateTime currentDay = DateTime.Now.Date;
        DateTime nextDay = currentDay.AddDays(1);

        return await _db.OrderLogs
            .AsNoTracking()
            .Where(o => o.OrderDate >= currentDay && o.OrderDate < nextDay)
            .ToListAsync();
    }

    public async Task<OrderLog> GetOrderLogByIdAsync(long orderLogId)
    {
        return await _db.OrderLogs
            .AsNoTracking()
            .FirstAsync(o => o.Id == orderLogId);
    }

    public async Task<IEnumerable<OrderLog>> GetOrderLogsByOrderHeaderIdAsync(int orderHeaderId)
    {
        return await _db.OrderLogs
            .AsNoTracking()
            .Where(o => o.OrderHeaderId == orderHeaderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderLog>> GetOrderLogsByUserIdAsync(string userId)
    {
        return await _db.OrderLogs
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<OrderLog> CreateOrderLogAsync(OrderLog orderLog)
    {
        _db.OrderLogs.Add(orderLog);
        await _db.SaveChangesAsync();
        return orderLog;
    }
}
