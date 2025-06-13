using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Product;
using Personal.Shopping.Web.Services.Interfaces;
using System.Collections.Generic;

namespace Personal.Shopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            List<ProductViewModel> listViewModel = new List<ProductViewModel>();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result!)!)!;
                listViewModel = ConvertToProductViewModelList(list);
            }

            return View(listViewModel);
        }

        public async Task<IActionResult> Details(int productId)
        {
            var product = new ProductViewModel();
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result!)!)!;
            }
            return View(product);
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

        private ProductViewModel ConvertToProductViewModel(ProductDto dto)
        {
            var response = _categoryService.GetCategoryByIdAsync(dto.ProductId).Result;
            var categoryDto = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result!)!)!;
            ProductViewModel model = new ProductViewModel
            {
                ProductId = dto.ProductId,
                Name = dto.Name,
                Description = dto.Description,
                Details = dto.Details,
                Price = dto.Price,
                CategoryName = categoryDto.CategoryName,
                ImageUrl = dto.ImageUrl
            };
            return model;
        }

        private List<ProductViewModel> ConvertToProductViewModelList(List<ProductDto> dtoList)
        {
            var modelList = new List<ProductViewModel>();
            foreach (var dto in dtoList) 
            {
                modelList.Add(ConvertToProductViewModel(dto));
            }

            return modelList;
        }
    }
}
