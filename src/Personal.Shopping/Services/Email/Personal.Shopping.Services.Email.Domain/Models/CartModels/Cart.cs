namespace Personal.Shopping.Services.Email.Domain.Models.CartModels;

public class Cart
{
    public CartHeader CartHeader { get; set; } = new CartHeader();
    public IEnumerable<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
