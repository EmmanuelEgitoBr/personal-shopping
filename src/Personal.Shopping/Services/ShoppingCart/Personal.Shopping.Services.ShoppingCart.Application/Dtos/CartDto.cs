namespace Personal.Shopping.Services.ShoppingCart.Application.Dtos;

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
    public IEnumerable<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto>();
}
