using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Personal.Shopping.Services.ShoppingCart.Application.Interfaces;

namespace Personal.Shopping.Services.ShoppingCart.Api.Controllers;

[Route("api/cart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly ICartHeaderService _cartHeaderService;
    private readonly ICartDetailService _cartDetailService;
    private readonly IProductService _productService;
    private readonly ICouponService _couponService;
    private ResponseDto _response;

    public ShoppingCartController(ICartHeaderService cartHeaderService,
        ICartDetailService cartDetailService,
        IProductService productService,
        ICouponService couponService)
    {
        _response = new ResponseDto();
        _cartHeaderService = cartHeaderService;
        _cartDetailService = cartDetailService;
        _productService = productService;
        _couponService = couponService;
    }

    [HttpGet("get-cart/{userId}")]
    public async Task<ResponseDto> GetCartByUserId(string userId)
    {
        try
        {
            var cartHeader = await _cartHeaderService.GetCartHeaderByUserIdAsync(userId);

            if(cartHeader == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Carrinho não encontrado!";
                return _response;
            }

            var cartDetails = _cartDetailService.GetCartDetailsByCartHeaderId(cartHeader.CartHeaderId);

            CartDto cartDto = new CartDto
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            };

            var responseDto = await _productService.GetAllProducs();
            IEnumerable<ProductDto> products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result)!)!;

            foreach (var item in cartDto.CartDetails)
            {
                item.Product = products.FirstOrDefault(p => p.ProductId == item.ProductId)!;
                cartDto.CartHeader.CartTotal += (item.Count * Convert.ToDouble(item.Product.Price));
            }

            //Aplly coupon
            if(!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
            {
                var couponCode = cartDto.CartHeader.CouponCode;
                var responseCouponDto = await _couponService.GetCouponAsync(couponCode);
                var couponDto = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseCouponDto.Result)!)!;

                if (couponDto != null && cartDto.CartHeader.CartTotal > couponDto.MinAmount)
                {
                    var oldPrice = cartDto.CartHeader.CartTotal;
                    var newPrice = oldPrice * (1 - ((couponDto.DiscountAmount) / 100));
                    cartDto.CartHeader.CartTotal = newPrice;
                    cartDto.CartHeader.Discount = oldPrice - newPrice;                }
            }

            _response.Result = cartDto;
            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message.ToString();
            _response.IsSuccess = false;
        }
        return _response;
    }

    [HttpPost("upsert-cart")]
    public async Task<ResponseDto> CartUpsert(CartDto cartDto)
    {
        try
        {
            var cartHeaderFromDb = await _cartHeaderService.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId!);
            if (cartHeaderFromDb == null)
            {
                //Create new header
                var cartHeaderId = await _cartHeaderService.CreateCartHeaderAsync(cartDto.CartHeader);
                //Create new details
                var cartDetail = cartDto.CartDetails.First();
                cartDetail.CartHeaderId = cartHeaderId;
                await _cartDetailService.CreateCartDeatilAsync(cartDetail);
            }
            else
            {
                var cartDetailFromDb = await _cartDetailService.GetCartDetailByProductIdAsync(cartDto.CartDetails, cartHeaderFromDb.CartHeaderId);

                if(cartDetailFromDb == null)
                {
                    //Create new details
                    var cartDetail = cartDto.CartDetails.First();
                    cartDetail.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    await _cartDetailService.CreateCartDeatilAsync(cartDetail);
                }
                else
                {
                    //Update count
                    var cartDetail = cartDto.CartDetails.First();
                    cartDetail.Count += cartDetailFromDb.Count;
                    cartDetail.CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cartDetail.CartDetailsId = cartDetailFromDb.CartDetailsId;
                    await _cartDetailService.UpdateCartDetailsAsync(cartDetail);
                }
            }
            _response.Result = cartDto;
            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message.ToString();
            _response.IsSuccess = false;
        }
        return _response;
    }

    [HttpPost("remove-cart")]
    public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailId)
    {
        try
        {
            var cartDetail = _cartDetailService.GetCartDetailByIdAsync(cartDetailId).Result;
            int totalCountOfCartItem = _cartDetailService.GetTotalItemsCountByCartHeaderId(cartDetailId);
            await _cartDetailService.RemoveCartDetailAsync(cartDetail);

            if (totalCountOfCartItem == 1)
            {
                var cartHeaderToRemove = await _cartHeaderService.GetCartHeaderByIdAsync(cartDetailId);
                await _cartHeaderService.RemoveCartHeaderAsync(cartHeaderToRemove);
            }

            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message.ToString();
            _response.IsSuccess = false;
        }
        return _response;
    }

    [HttpPost("apply-coupon")]
    public async Task<ResponseDto> ApplyCoupon([FromBody]CartDto cartDto)
    {
        try
        {            
            var cartHeaderFromDb = _cartHeaderService.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId!).Result;
            var couponCode = cartDto.CartHeader.CouponCode!.ToUpper();
            cartHeaderFromDb.CouponCode = couponCode;

            await _cartHeaderService.UpdateCartHeaderAsync(cartHeaderFromDb)!;

            _response.Result = cartHeaderFromDb;
            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message.ToString();
            _response.IsSuccess = false;
        }
        return _response;
    }

    [HttpPost("remove-coupon")]
    public async Task<ResponseDto> RemoveCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            var cartHeaderFromDb = _cartHeaderService.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserId!).Result;
            
            cartHeaderFromDb.CouponCode = null;
            await _cartHeaderService.UpdateCartHeaderAsync(cartHeaderFromDb)!;

            _response.Result = cartHeaderFromDb;
            _response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message.ToString();
            _response.IsSuccess = false;
        }
        return _response;
    }

}
