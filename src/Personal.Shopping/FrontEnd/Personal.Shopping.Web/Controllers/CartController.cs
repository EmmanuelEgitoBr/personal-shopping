using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Order;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Models.Stripe;
using Personal.Shopping.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Personal.Shopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(IShoppingCartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var cartDto = await LoadCartDtoBasedOnLoggedInUser();

            return View(cartDto);
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cartDto = await LoadCartDtoBasedOnLoggedInUser();

            return View(cartDto);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            var cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.FirstName = cartDto.CartHeader.FirstName;
            cart.CartHeader.LastName = cartDto.CartHeader.LastName;

            var response = await _orderService.CreateOrderAsync(cart);
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();

            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result)!)!;

                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "cart/confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto,
                };

                var stripeResponse = await _orderService.CreateStripeSessionAsync(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result)!)!;

                if(stripeResponseResult != null)
                {

                    Response.Headers.Append("Location", stripeResponseResult.StripeSessionUrl);
                    return new StatusCodeResult(303);
                }
            }
            return View();
        }

        [HttpGet, HttpPost]
        public async Task<ActionResult> Confirmation(int orderId)
        {
            ResponseDto responseDto = await _orderService.ValidateStripeSessionAsync(orderId);

            if (responseDto.IsSuccess)
            {
                OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(responseDto!.Result)!)!;
                if (orderHeaderDto.Status == "Approved") 
                {
                    return View(orderId);
                }
            }

            return View(orderId);
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
        public async Task<IActionResult> EmailCart(CartDto model)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;

            ResponseDto responseDto = await _cartService.EmailCartAsync(cart);

            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["Message"] = "Email enviado com sucesso!";
                TempData["MessageType"] = "success";
                return RedirectToAction(nameof(CartIndex));
            }
            TempData["Message"] = "Erro ao enviar email!";
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
