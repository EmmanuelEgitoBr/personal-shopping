using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Product;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result!)!)!;
            }

            return View(list);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _productService.CreateProductAsync(model);

                if (response is not null && response.IsSuccess)
                {
                    TempData["Message"] = "Produto criado com sucesso!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ProductDto productDto = new();

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result!)!)!;
                return View(productDto);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            ResponseDto? response = await _productService.UpdateProductAsync(model);

            if (response.IsSuccess)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ProductDto productDto = new();

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result!)!)!;
                return View(productDto);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(model.ProductId);

            if (response.IsSuccess)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(model);
        }
    }
}
