using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.OrderManager.Web.Services.Interfaces;

namespace Personal.Shopping.OrderManager.Web.Controllers
{
    public class OrderLogController : Controller
    {
        private readonly IOrderLogApiClient _orderLogService;

        public OrderLogController(IOrderLogApiClient orderLogService)
        {
            _orderLogService = orderLogService;
        }

        public IActionResult OrderLogIndex()
        {
            return View();
        }
    }
}
