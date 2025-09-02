using Personal.Shopping.OrderManager.Web.Models;
using Personal.Shopping.OrderManager.Web.Models.Order;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface IOrderLogApiClient
{
    [Get("/api/order-logs")]
    Task<ResponseDto> GetAllOrderLogsAsync();

    [Get("/api/order-logs/today")]
    Task<ResponseDto> GetOrderLogsByCurrentDayAsync();

    [Get("/api/order-logs/log/{id}")]
    Task<ResponseDto> GetOrderLogsByLogIdAsync(long id);

    [Get("/api/order-logs/user/{id}")]
    Task<ResponseDto> GetOrderLogsByUserIdAsync(string id);

    [Get("/api/order-logs/order/{id}")]
    Task<ResponseDto> GetOrderLogsByOrderHeaderIdAsync(int id);

    [Post("/api/order-logs")]
    Task<ResponseDto> CreateOrderLogAsync([Body] OrderLogDto orderLogDto);
}
