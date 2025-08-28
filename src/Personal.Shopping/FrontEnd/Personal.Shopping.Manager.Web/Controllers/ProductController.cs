using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Manager.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductIndex()
        {
            return View();
        }
    }
}
