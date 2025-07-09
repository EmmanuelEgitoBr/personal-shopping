using Personal.Shopping.Web.Models.Product;

namespace Personal.Shopping.Web.Models.ShoppingCart;

public class CartDetailDto
{
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
    public int ProductId { get; set; }
    public ProductDto Product { get; set; } = new ProductDto();
    public int Count { get; set; }
}
