namespace Personal.Shopping.Integration.MessageBus.Interfaces;

public interface IKafkaProducerService<T>
{
    Task PublishOrderAsync(T order);
}
