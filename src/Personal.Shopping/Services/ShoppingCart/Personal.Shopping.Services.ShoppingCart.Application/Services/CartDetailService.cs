using AutoMapper;
using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Personal.Shopping.Services.ShoppingCart.Application.Interfaces;
using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Domain.Entities;

namespace Personal.Shopping.Services.ShoppingCart.Application.Services;

public class CartDetailService : ICartDetailService
{
    private readonly ICartDetailRepository _cartDetailRepository;
    private readonly IMapper _mapper;

    public CartDetailService(ICartDetailRepository cartDetailRepository, IMapper mapper)
    {
        _cartDetailRepository = cartDetailRepository;
        _mapper = mapper;
    }

    public async Task CreateCartDeatilAsync(CartDetailDto cartDetailDto)
    {
        var cartDetail = _mapper.Map<CartDetail>(cartDetailDto);
        await _cartDetailRepository.CreateCartDeatil(cartDetail);
    }

    public async Task<CartDetailDto> GetCartDetailByIdAsync(int cartDetailId)
    {
        var cartDetail = await _cartDetailRepository.GetCartDetailById(cartDetailId);
        return _mapper.Map<CartDetailDto>(cartDetail);
    }

    public async Task<CartDetailDto> GetCartDetailByProductIdAsync(IEnumerable<CartDetailDto> cartDetailsDto, int cardHeaderId)
    {
        var productId = cartDetailsDto.First().ProductId;
        var cartDetail = await _cartDetailRepository.GetCartDetailByProductId(productId, cardHeaderId);
        var cartDetailDto = _mapper.Map<CartDetailDto>(cartDetail);

        return cartDetailDto;
    }

    public IEnumerable<CartDetailDto> GetCartDetailsByCartHeaderId(int cartHeaderId)
    {
        var carDetails = _cartDetailRepository.GetCartDetailsByCartHeaderId(cartHeaderId);
        return _mapper.Map<IEnumerable<CartDetailDto>>(carDetails);
    }

    public int GetTotalItemsCountByCartHeaderId(int cartHeaderId)
    {
        return _cartDetailRepository.GetTotalItemsCountByCartHeaderId(cartHeaderId);
    }

    public async Task RemoveCartDetailAsync(CartDetailDto cartDetailDto)
    {
        var cartDetail = _mapper.Map<CartDetail>(cartDetailDto);
        await _cartDetailRepository.RemoveCartDetail(cartDetail);
    }

    public async Task UpdateCartDetailsAsync(CartDetailDto cartDetailDto)
    {
        var cartDetail = new CartDetail()
        {
            CartDetailsId = cartDetailDto.CartDetailsId,
            CartHeaderId = cartDetailDto.CartHeaderId,
            ProductId = cartDetailDto.ProductId,
            Count = cartDetailDto.Count
        };
        await _cartDetailRepository.UpdateCartDetails(cartDetail);
    }
}
