namespace Personal.Shopping.Services.Email.Application.Models;

public class SnSMessageEnvelope
{
    public string? Type { get; set; }
    public string? MessageId { get; set; }
    public string? TopicArn { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; }
    public string? SignatureVersion { get; set; }
    public string? Signature { get; set; }
    public string? SignatureCertUrl { get; set; }
    public string? UnsubscribeUrl { get; set; }
}
