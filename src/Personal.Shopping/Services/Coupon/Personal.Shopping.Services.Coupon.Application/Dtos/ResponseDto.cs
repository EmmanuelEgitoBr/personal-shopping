namespace Personal.Shopping.Services.Coupon.Application.Dtos;

public class ResponseDto<T> where T : class
{
    public T? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
