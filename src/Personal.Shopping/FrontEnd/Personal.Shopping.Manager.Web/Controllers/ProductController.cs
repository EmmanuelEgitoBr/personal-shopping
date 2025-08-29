using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Product;
using Personal.Shopping.Manager.Web.Services.Interfaces;

namespace Personal.Shopping.Manager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productService;
        private readonly ICategoryApiClient _categoryService;

        public ProductController(IProductApiClient productService,
            ICategoryApiClient categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            List<ProductViewModel> listViewModel = new();
            CategoryDto categoryDto = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result!)!)!;

                foreach (ProductDto productDto in list)
                {
                    var categoryResult = await _categoryService.GetCategoryByIdAsync(productDto.CategoryNameId);
                    categoryDto = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(categoryResult.Result!)!)!;

                    ProductViewModel viewModel = new()
                    {
                        Product = productDto,
                        CategoryName = categoryDto.CategoryName
                    };
                    listViewModel.Add(viewModel);
                }
            }

            return View(listViewModel);
        }

        public async Task<IActionResult> Details(int productId)
        {
            var productModel = new ProductViewModel();
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result!)!)!;
                //productModel = ConvertToProductViewModel(product);
            }
            return View(productModel);
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
