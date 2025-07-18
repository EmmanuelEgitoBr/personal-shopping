using Personal.Shopping.Services.Order.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Shopping.Services.Order.Domain.Entity;

public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }
    public int OrderHeaderId { get; set; }
    public OrderHeader CartHeader { get; set; } = new OrderHeader();
    public int ProductId { get; set; }

    [NotMapped]
    public Product? Product { get; set; }
    public int Count { get; set; }
    public string? ProductName { get; set; }
    public double Price { get; set; }
}
