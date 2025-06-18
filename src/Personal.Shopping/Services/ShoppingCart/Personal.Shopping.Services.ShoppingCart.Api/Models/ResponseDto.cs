namespace Personal.Shopping.Services.ShoppingCart.Api.Models;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
