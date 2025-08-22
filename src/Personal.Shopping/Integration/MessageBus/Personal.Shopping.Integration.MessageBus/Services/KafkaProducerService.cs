using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Personal.Shopping.Integration.MessageBus.Interfaces;
using System.Text.Json;

namespace Personal.Shopping.Integration.MessageBus.Services
{
    public class KafkaProducerService<T> : IKafkaProducerService<T>
    {
        private readonly IProducer<Null, string> _producer;
        private const string Topic = "ordersCreated";

        public KafkaProducerService(IConfiguration config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"] ?? "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task PublishOrderAsync(T order)
        {
            var message = JsonSerializer.Serialize(order);
            await _producer.ProduceAsync(Topic, new Message<Null, string> { Value = message });
        }
    }
}
