namespace Personal.Shopping.Services.ShoppingCart.Application.Dtos;

public class CouponDto
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime StartsIn { get; set; }
    public DateTime ExpiresIn { get; set; }
}
