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
        public async Task<ResponseDto> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();

            return result;
        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<ActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);

            return Ok(result);
        }

        [HttpGet("get-by-name/{productName}")]
        public async Task<ActionResult> GetProductByName(string productName)
        {
            var result = await _productService.GetProductByNameAsync(productName.ToUpper());

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
            var result = await _productService.DeleteProductAsync(id);

            return Ok(result);
        }

        [HttpPost("{id}/upload-image")]
        public async Task<ActionResult> UploadProductImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Arquivo inválido");
            
            var result = await _productService.UploadProductImageAsync(id, file);

            return Ok(result);
        }
    }
}
