using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Configurations.Resources;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Order;
using Personal.Shopping.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Personal.Shopping.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
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
    }
}
