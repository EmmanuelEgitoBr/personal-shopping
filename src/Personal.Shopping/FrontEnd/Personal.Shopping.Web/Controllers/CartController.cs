using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Personal.Shopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public CartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var cartDto = await LoadCartDtoBasedOnLoggedInUser();

            return View(cartDto);
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto responseDto = await _cartService.GetCartByUserIdAsync(userId!);

            if (responseDto != null && responseDto.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseDto.Result)!)!;
                return cartDto;
            }
            return new CartDto();
        }
    }
}
