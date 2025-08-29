using Microsoft.AspNetCore.Mvc.Rendering;

namespace Personal.Shopping.Manager.Web.Models.Product;

public class ProductViewModel
{
    public ProductDto? Product { get; set; }
    public string? CategoryName { get; set; }
    public SelectList? Categories { get; set; }
}
