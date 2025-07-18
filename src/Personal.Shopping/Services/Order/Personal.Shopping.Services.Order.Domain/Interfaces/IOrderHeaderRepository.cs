using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Domain.Interfaces;

public interface IOrderHeaderRepository
{
    Task<OrderHeader> CreateCartHeader(OrderHeader cartHeader);
}
