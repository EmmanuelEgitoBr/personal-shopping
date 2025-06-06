using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Product.Application.Interfaces;

namespace Personal.Shopping.Services.Product.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCoupons()
        {
            //var result = await _productService.GetAllProductsAsync();

            return Ok();
        }
    }
}
