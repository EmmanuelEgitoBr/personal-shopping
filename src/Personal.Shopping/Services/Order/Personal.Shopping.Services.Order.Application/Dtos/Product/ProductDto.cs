namespace Personal.Shopping.Services.Order.Application.Dtos.Product;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryNameId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
