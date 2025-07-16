namespace Personal.Shopping.Integration.MessageBus.Interfaces;

public interface IMessageBus
{
    Task PublishMessageAsync(object message, string topicQueueName);
}
