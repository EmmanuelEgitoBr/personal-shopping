namespace Personal.Shopping.Integration.MessageBus.Models;

public record OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string? Email { get; set; }
}
