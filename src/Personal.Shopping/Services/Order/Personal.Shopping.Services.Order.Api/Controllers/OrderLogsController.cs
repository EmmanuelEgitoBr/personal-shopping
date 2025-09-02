using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Interfaces;

namespace Personal.Shopping.Services.Order.Api.Controllers
{
    [Route("api/order-logs")]
    [ApiController]
    public class OrderLogsController : ControllerBase
    {
        private readonly IOrderLogService _orderLogService;

        public OrderLogsController(IOrderLogService orderLogService)
        {
            _orderLogService = orderLogService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrderLogs()
        {
            var result = await _orderLogService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("today")]
        public async Task<ActionResult> GetOrderLogsByCurrentDay()
        {
            var result = await _orderLogService.GetAllOrdersByCurrentDayAsync();
            return Ok(result);
        }

        [HttpGet("log/{id}")]
        public async Task<ActionResult> GetOrderLogsByOrderId(long id)
        {
            var result = await _orderLogService.GetOrderLogsByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetOrderLogsByUserId(string id)
        {
            var result = await _orderLogService.GetOrderLogsByUserIdAsync(id);
            return Ok(result);
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult> GetOrderLogsByOrderId(int id)
        {
            var result = await _orderLogService.GetOrderLogsByOrderHeaderIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderLog(OrderLogDto orderLogDto)
        {
            var result = await _orderLogService.CreateOrderLogAsync(orderLogDto);
            return Ok(result);
        }
    }
}
