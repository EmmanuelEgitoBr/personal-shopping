using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Product.Application.Dtos;
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
        public async Task<ActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();

            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("get-by-code/{name}")]
        public async Task<ActionResult> GetProductByName(string name)
        {
            var result = await _productService.GetProductByNameAsync(name.ToUpper());

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var result = await _productService.CreateProductAsync(productDto);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var result = await _productService.UpdateProductAsync(productDto);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);

            return Ok();
        }
    }
}
