using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Domain.Interfaces;

public interface IOrderHeaderRepository
{
    Task<OrderHeader> GetOrderHeaderByIdAsync(int orderHeaderId);
    Task<OrderHeader> CreateCartHeader(OrderHeader cartHeader);
    Task UpdateOrderHeaderAsync(OrderHeader orderHeader);
}
