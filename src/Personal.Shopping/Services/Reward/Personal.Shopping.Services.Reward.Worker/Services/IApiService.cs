using Personal.Shopping.Services.Reward.Worker.Models;
using Refit;

namespace Personal.Shopping.Services.Reward.Worker.Services;

public interface IApiService
{
    [Post("/email/send-email")]
    Task<ApiResponse<string>> ProcessOrderAsync([Body] KafkaMessageEnvelope message);
}
