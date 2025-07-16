using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Personal.Shopping.Integration.MessageBus.Interfaces;

namespace Personal.Shopping.Integration.MessageBus;

public class MessageBus : IMessageBus
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IAmazonSimpleNotificationService _snsClient;

    public MessageBus(IAmazonSQS sqsClient, IAmazonSimpleNotificationService snsClient)
    {
        _sqsClient = sqsClient;
        _snsClient = snsClient;
    }

    public async Task PublishMessage(object message, string topicQueueName)
    {
        var jsonMessage = JsonConvert.SerializeObject(message);

        if (topicQueueName.StartsWith("arn:aws:sns"))
        {
            await _snsClient.PublishAsync(new PublishRequest
            {
                TopicArn = topicQueueName,
                Message = jsonMessage
            });
        }
        else if (topicQueueName.StartsWith("https://sqs."))
        {
            await _sqsClient.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = topicQueueName,
                MessageBody = jsonMessage
            });
        }
        else
        {
            throw new ArgumentException("Identificador inválido. Use ARN do SNS ou URL do SQS.");
        }

        throw new NotImplementedException();
    }
}
