namespace Personal.Shopping.OrderManager.Web.Models.ShoppingCart;

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
    public IEnumerable<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto>();
}
