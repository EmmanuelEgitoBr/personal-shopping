namespace Personal.Shopping.Services.Email.Application.Models;

public class KafkaMessageEnvelope
{
    public string OrderId { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
