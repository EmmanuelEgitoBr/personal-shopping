using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.Manager.Web.Models.Product;

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    [Range(1, 1000)]
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    [Range(1, 100)]
    public int Count { get; set; } = 1;
}
