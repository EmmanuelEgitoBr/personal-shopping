using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.Services.ShoppingCart.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    [Range(1, 1000)]
    public decimal Price { get; set; }
    public int CategoryNameId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
