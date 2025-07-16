namespace Personal.Shopping.Integration.MessageBus.Interfaces;

public interface IMessageBus
{
    Task PublishMessage(object message, string topicQueueName);
}
