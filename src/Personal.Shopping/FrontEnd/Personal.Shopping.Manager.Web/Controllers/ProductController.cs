using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            
            var categories = LoadCategories().Result;

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
                        CategoryName = categoryDto.CategoryName,
                        Categories = new SelectList(categories, "CategoryNameId", "CategoryName")
                    };
                    listViewModel.Add(viewModel);
                }
            }

            return View(listViewModel);
        }

        public async Task<IActionResult> ProductCreate()
        {
            var categories = await LoadCategories();

            var model = new ProductViewModel
            {
                Product = new ProductDto(),
                Categories = new SelectList(categories, "CategoryNameId", "CategoryName")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _productService.CreateProductAsync(model.Product);

                if (response is not null && response.IsSuccess)
                {
                    TempData["Message"] = "Produto criado com sucesso!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("ProductIndex");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int id)
        {
            var categories = LoadCategories().Result;

            ProductDto productDto = new();

            ResponseDto? response = await _productService.GetProductByIdAsync(id);

            if (response is not null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result!)!)!;
                
                ProductViewModel viewModel = new()
                {
                    Product = productDto,
                    Categories = new SelectList(categories, "CategoryNameId", "CategoryName", productDto.CategoryNameId)
                };
                
                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await LoadCategories();
                model.Categories = new SelectList(categories, 
                    "CategoryNameId", 
                    "CategoryName", 
                    model.Product.CategoryNameId);
                return View(model);
            }

            await _productService.UpdateProductAsync(model.Product);
            return RedirectToAction(nameof(ProductIndex));
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(id);

            if (response.IsSuccess)
            {
                TempData["Message"] = "Produto excluído com sucesso!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Erro ao excluir produto!";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(ProductIndex));
        }

        private async Task<List<CategoryDto>> LoadCategories()
        {
            var categoriesResult = await _categoryService.GetAllCategoriesAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(categoriesResult.Result!)!)!;

            return categories;
        }
    }
}
