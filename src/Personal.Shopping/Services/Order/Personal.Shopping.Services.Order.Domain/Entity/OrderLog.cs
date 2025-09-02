namespace Personal.Shopping.Services.Order.Domain.Entity;

public class OrderLog
{
    public long Id { get; set; }
    public int OrderHeaderId {  get; set; }
    public string? OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string? LogDescription { get; set; }
}
