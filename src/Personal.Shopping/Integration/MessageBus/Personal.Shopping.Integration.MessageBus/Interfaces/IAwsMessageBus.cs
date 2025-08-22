namespace Personal.Shopping.Integration.MessageBus.Interfaces;

public interface IAwsMessageBus
{
    Task PublishMessageAsync(object message, string topicQueueName);
}
