using AutoMapper;
using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Personal.Shopping.Services.ShoppingCart.Application.Interfaces;
using Personal.Shopping.Services.ShoppingCart.Domain.Contracts;
using Personal.Shopping.Services.ShoppingCart.Domain.Entities;

namespace Personal.Shopping.Services.ShoppingCart.Application.Services;

public class CartHeaderService : ICartHeaderService
{
    private readonly ICartHeaderRepository _cartHeaderRepository;
    private readonly IMapper _mapper;

    public CartHeaderService(ICartHeaderRepository cartHeaderRepository, IMapper mapper)
    {
        _cartHeaderRepository = cartHeaderRepository;
        _mapper = mapper;
    }

    public async Task CreateCartHeaderAsync(CartHeaderDto cartHeaderDto)
    {
        var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);

        await _cartHeaderRepository.CreateCartHeader(cartHeader);
    }

    public async Task<CartHeaderDto> GetCartHeaderByIdAsync(int cartHeaderId)
    {
        var cartHeader = await _cartHeaderRepository.GetCartHeaderById(cartHeaderId);
        return _mapper.Map<CartHeaderDto>(cartHeader);
    }

    public async Task<CartHeaderDto> GetCartHeaderByUserIdAsync(string userId)
    {
        var cartHeader = await _cartHeaderRepository.GetCartHeaderByUserId(userId);
        var cartHeaderDto = _mapper.Map<CartHeaderDto>(cartHeader);

        return cartHeaderDto;
    }

    public async Task RemoveCartHeaderAsync(CartHeaderDto cartHeaderDto)
    {
        var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);
        await _cartHeaderRepository.RemoveCartHeader(cartHeader);
    }

    public async Task UpdateCartHeaderAsync(CartHeaderDto cartHeaderDto)
    {
        var cartHeader = _mapper.Map<CartHeader>(cartHeaderDto);
        await _cartHeaderRepository.UpdateCartHeader(cartHeader);
    }
}
