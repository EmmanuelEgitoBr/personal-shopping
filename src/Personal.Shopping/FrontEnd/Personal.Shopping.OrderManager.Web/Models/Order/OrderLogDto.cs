namespace Personal.Shopping.OrderManager.Web.Models.Order;

public class OrderLogDto
{
    public long Id { get; set; }
    public int OrderHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string? LogDescription { get; set; }
}
