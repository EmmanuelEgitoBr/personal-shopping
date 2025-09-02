using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Domain.Interfaces;

public interface IOrderLogRepository
{
    Task<IEnumerable<OrderLog>> GetAllOrdersAsync();
    Task<IEnumerable<OrderLog>> GetAllOrdersByCurrentDayAsync();
    Task<IEnumerable<OrderLog>> GetOrderLogsByOrderHeaderIdAsync(int orderHeaderId);
    Task<IEnumerable<OrderLog>> GetOrderLogsByUserIdAsync(string userId);
    Task<OrderLog> CreateOrderLogAsync(OrderLog orderLog);
    Task<OrderLog> GetOrderLogByIdAsync(long orderLogId);
}
