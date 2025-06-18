using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.ShoppingCart.Api.Models;
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
    private ResponseDto _response;

    public ShoppingCartController(ICartHeaderService cartHeaderService,
        ICartDetailService cartDetailService,
        IProductService productService)
    {
        _response = new ResponseDto();
        _cartHeaderService = cartHeaderService;
        _cartDetailService = cartDetailService;
        _productService = productService;
    }

    [HttpGet("get-cart/{userId}")]
    public async Task<ResponseDto> GetCartByUserId(string userId)
    {
        try
        {
            var cartHeader = await _cartHeaderService.GetCartHeaderByUserIdAsync(userId);
            var cartDetails = _cartDetailService.GetCartDetailsByCartHeaderId(cartHeader.CartHeaderId);

            CartDto cartDto = new CartDto
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            };

            IEnumerable<ProductDto> products = _productService.GetAllProducs().Result;

            foreach (var item in cartDto.CartDetails)
            {
                item.Product = products.FirstOrDefault(p => p.ProductId == item.ProductId)!;
                cartDto.CartHeader.CartTotal += (item.Count * Convert.ToDouble(item.Product.Price));
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
                await _cartHeaderService.CreateCartHeaderAsync(cartDto.CartHeader);
                //Create new details
                var cartDetail = cartDto.CartDetails.First();
                cartDetail.CartHeaderId = cartDto.CartHeader.CartHeaderId;
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

}
