using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.OrderManager.Web.Configurations.Resources;
using Personal.Shopping.OrderManager.Web.Models;
using Personal.Shopping.OrderManager.Web.Models.Enums;
using Personal.Shopping.OrderManager.Web.Models.Order;
using Personal.Shopping.OrderManager.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Personal.Shopping.OrderManager.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderService;

        public OrderController(IOrderApiClient orderService)
        {
            _orderService = orderService;
        }

        public IActionResult OrderIndex()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            ResponseDto response = new();
            IEnumerable<OrderHeaderDto> list = new List<OrderHeaderDto>();

            if (!User.IsInRole(RoleConstants.RoleAdmin))
            {
                string userId = User.Claims!
                    .Where(u => u.Type == JwtRegisteredClaimNames.Sub)!
                    .FirstOrDefault()!.Value;

                response = await _orderService.GetOrdersByUserId(userId);
            }
            else
                response = await _orderService.GetAllOrders();

            if (response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response!.Result!)!)!;
            }

            return Json(new { data = list });
        }

        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new();
            string userId = User.Claims!
                    .Where(u => u.Type == JwtRegisteredClaimNames.Sub)!
                    .FirstOrDefault()!.Value;

            var response = await _orderService.GetOrderByOrderHeaderId(orderId);

            if (response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response!.Result!)!)!;
            }

            return View(orderHeaderDto);
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderHeaderId)
        {
            var response = await _orderService.UpdateOrderStatus(StatusTypes.Status_Cancelled, orderHeaderId);

            if (response.IsSuccess)
            {
                TempData["Message"] = "Solicitação de cancelamento feito com sucesso";
                TempData["MessageType"] = "success";

                return RedirectToAction(nameof(OrderIndex));
            }

            return View();
        }

        public IActionResult OrderLog()
        {
            return View();
        }
    }
}
