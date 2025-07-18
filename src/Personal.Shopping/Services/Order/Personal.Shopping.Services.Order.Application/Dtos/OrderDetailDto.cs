using Personal.Shopping.Services.Order.Application.Dtos.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Shopping.Services.Order.Application.Dtos;

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
