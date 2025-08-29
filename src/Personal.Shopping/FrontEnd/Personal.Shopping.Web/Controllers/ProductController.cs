using Duende.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Product;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IShoppingCartService _shoppingCartService;

        public ProductController(IProductService productService, 
            ICategoryService categoryService,
            IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _shoppingCartService = shoppingCartService;
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
            var productModel = new ProductViewModel();
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response is not null && response.IsSuccess)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result!)!)!;
                productModel = ConvertToProductViewModel(product);
            }
            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(ProductViewModel model)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()!.Value
                }
            };

            CartDetailDto cartDetailDto = new CartDetailDto
            {
                Count = model.Count,
                ProductId = model.ProductId,
            };

            List<CartDetailDto> cartDetailsDto = new() { cartDetailDto };
            cartDto.CartDetails = cartDetailsDto;

            ResponseDto responseDto = await _shoppingCartService.CartUpsertAsync(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["Message"] = "Produto adicionado ao carrinho!";
                TempData["MessageType"] = "success";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "Erro ao adicionar produto no carrinho!";
            TempData["MessageType"] = "error";

            return RedirectToAction("Index");
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
