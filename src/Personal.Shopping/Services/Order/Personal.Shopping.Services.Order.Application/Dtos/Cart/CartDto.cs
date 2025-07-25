﻿namespace Personal.Shopping.Services.Order.Application.Dtos.Cart;

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
    public IEnumerable<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto>();
}
