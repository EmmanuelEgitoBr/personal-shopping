using Personal.Shopping.Services.Email.Domain.Models.ProductModels;

namespace Personal.Shopping.Services.Email.Domain.Models.CartModels;

public class CartDetail
{
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeader CartHeader { get; set; } = new CartHeader();
    public int ProductId { get; set; }
    public Product Product { get; set; } = new Product();
    public int Count { get; set; }
}
