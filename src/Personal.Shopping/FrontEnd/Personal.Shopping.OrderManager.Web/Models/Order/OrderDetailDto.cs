using Personal.Shopping.OrderManager.Web.Models.Product;

namespace Personal.Shopping.OrderManager.Web.Models.Order;

public class OrderDetailDto
{
    public int OrderDetailId { get; set; }
    public int OrderHeaderId { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
    public string? ProductName { get; set; }
    public double Price { get; set; }
}
