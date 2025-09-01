using Microsoft.AspNetCore.Mvc.Rendering;

namespace Personal.Shopping.Manager.Web.Models.Product;

public class ProductViewModel
{
    public ProductDto Product { get; set; } = new ProductDto();
    public IFormFile? ImageFile { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public SelectList? Categories { get; set; }
}
