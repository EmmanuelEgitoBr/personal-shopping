using Personal.Shopping.Services.Reward.Worker.Models;
using Refit;

namespace Personal.Shopping.Services.Reward.Worker.Services;

public interface IApiService
{
    [Post("/api/email/send-email")]
    Task<ApiResponse<KafkaMessageEnvelope>> SendEmailAsync([Body] KafkaMessageEnvelope message);
}
