﻿namespace Personal.Shopping.Services.ShoppingCart.Application.Dtos;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
