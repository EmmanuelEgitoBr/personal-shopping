using Confluent.Kafka;
using Personal.Shopping.Services.Reward.Worker.Builders;
using Personal.Shopping.Services.Reward.Worker.Models;
using Personal.Shopping.Services.Reward.Worker.Services;
using System.Text.Json;

namespace Personal.Shopping.Services.Reward.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IApiService _apiService;

        public Worker(ILogger<Worker> logger, 
            IConfiguration configuration,
            IApiService apiService)
        {
            _logger = logger;
            _configuration = configuration;
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
                GroupId = "reward-service-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
            consumer.Subscribe("ordersCreated");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    var order = JsonSerializer.Deserialize<RewardsDto>(result.Message.Value);
                    
                    var message = EmailBuilder.BuildEmailBody(order!);
                    var sendEmail = await _apiService.SendEmailAsync(message);

                    _logger.LogInformation($"[RewardApi] Pedido recebido: {order?.OrderId} - Cliente: {order?.OrderId}");
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Erro no consumo: {ex.Error.Reason}");
                }
            }
        }
    }
}
