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

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto responseDto = await _cartService.RemoveCartAsync(cartDetailsId);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["Message"] = "Carrinho excluído com sucesso!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto responseDto = await _cartService.ApplyCouponAsync(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["Message"] = "Cupom aplicado com sucesso!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(CartIndex));
            }
            TempData["Message"] = "Erro ao aplicar cupom!";
            TempData["MessageType"] = "error";
            return RedirectToAction(nameof(CartIndex));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            ResponseDto responseDto = await _cartService.RemoveCouponAsync(cartDto);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["Message"] = "Cupom removido com sucesso!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(CartIndex));
            }
            TempData["Message"] = "Erro ao remover cupom!";
            TempData["MessageType"] = "error";
            return RedirectToAction(nameof(CartIndex));
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
