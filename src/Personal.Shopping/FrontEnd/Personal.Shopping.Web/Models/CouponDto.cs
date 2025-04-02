namespace Personal.Shopping.Web.Models;

public class CouponDto
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime StartsIn { get; set; }
    public DateTime ExpiresIn { get; set; }
}
