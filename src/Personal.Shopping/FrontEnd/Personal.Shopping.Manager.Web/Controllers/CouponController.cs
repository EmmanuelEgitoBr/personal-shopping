using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Manager.Web.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult CouponIndex()
        {
            return View();
        }
    }
}
