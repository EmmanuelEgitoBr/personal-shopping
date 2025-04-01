using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.Services.Coupon.Domain.Entities;

public class Coupon
{
    [Key]
    public int CouponId { get; set; }
    [Required]
    public string CouponCode { get; set; } = string.Empty;
    [Required]
    public double DiscountAmount { get; set; }
    [Required]
    public int MinAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public DateTime StartsIn { get; set; }
    [Required]
    public DateTime ExpiresIn { get; set; }
}
