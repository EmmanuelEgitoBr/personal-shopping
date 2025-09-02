using Personal.Shopping.Services.Order.Application.Dtos;

namespace Personal.Shopping.Services.Order.Application.Interfaces;

public interface IOrderLogService
{
    Task<ResponseDto> GetAllOrdersAsync();
    Task<ResponseDto> GetAllOrdersByCurrentDayAsync();
    Task<ResponseDto> GetOrderLogsByIdAsync(long orderLogId);
    Task<ResponseDto> GetOrderLogsByOrderHeaderIdAsync(int orderHeaderId);
    Task<ResponseDto> GetOrderLogsByUserIdAsync(string userId);
    Task<ResponseDto> CreateOrderLogAsync(OrderLogDto orderLogDto);
}
