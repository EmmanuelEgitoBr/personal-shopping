﻿namespace Personal.Shopping.Services.Coupon.Application.Dtos;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
