using Personal.Shopping.Services.Reward.Worker.Models;

namespace Personal.Shopping.Services.Reward.Worker.Builders;

public static class EmailBuilder
{
    public static KafkaMessageEnvelope BuildEmailBody(RewardsDto rewards)
    {
        var subject = $"Novo pedido #{rewards.OrderId}";
        var body = $"Cliente: {rewards.UserId}\n" +
                   $"Valor: {rewards.RewardsActivity}\n" +
                   $"Data: {DateTime.Now}";

        var message = new KafkaMessageEnvelope()
        {
            Subject = subject,
            Body = body,
            To = "e_egito@hotmail.com",
            OrderId = rewards.OrderId.ToString(),
        };

        return message;
    }
}
