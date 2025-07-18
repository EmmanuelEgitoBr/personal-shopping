using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Personal.Shopping.Services.Email.Application.Interfaces;
using Personal.Shopping.Services.Email.Application.Models;
using Personal.Shopping.Services.Email.Domain.Interfaces;
using Personal.Shopping.Services.Email.Domain.Models.CartModels;
using Personal.Shopping.Services.Email.Infra.Repositories;

namespace Personal.Shopping.Services.Email.Application.Services.Messaging;

public class ServiceBusConsumer : IServiceBusConsumer
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ServiceBusConsumer> _logger;
    private readonly EmailRepository _emailRepository;
    private readonly string _queueUrl;
    private CancellationTokenSource _cts;
    private Task? _pollingTask;

    public ServiceBusConsumer(IAmazonSQS sqsClient, 
                              IConfiguration configuration,
                              ILogger<ServiceBusConsumer> logger,
                              EmailRepository emailRepository)
    {
        _sqsClient = sqsClient;
        _configuration = configuration;
        _logger = logger;
        _emailRepository = emailRepository;
        _cts = new CancellationTokenSource();

        // Obtenha a URL da fila usando o nome da fila
        var queueName = _configuration.GetSection("Queue:EmailShoppingCart").Value;
        _queueUrl = _sqsClient.GetQueueUrlAsync(queueName).Result.QueueUrl;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _pollingTask = Task.Run(() => PollMessagesAsync(_cts.Token), cancellationToken);
        return Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        _cts.Cancel();
        await _pollingTask!;
    }

    private async Task PollMessagesAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 5,
                WaitTimeSeconds = 10
            };

            var response = await _sqsClient.ReceiveMessageAsync(request, cancellationToken);

            if (response.Messages is not null && response.Messages.Count > 0)
            {
                foreach (var message in response.Messages)
                {
                    try
                    {
                        // Deserialize e processa a mensagem
                        var envelope = JsonConvert.DeserializeObject<SnSMessageEnvelope>(message.Body);
                        var cart = JsonConvert.DeserializeObject<Cart>(envelope!.Message!);

                        await _emailRepository.EmailCartAndLog(cart!);
                        _logger.LogInformation($"Recebido: {message.Body}");

                        // Apagar da fila após processar
                        await _sqsClient.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                        // opcional: não deletar a mensagem para retry automático
                    }
                }
            }
            else
            {
                _logger.LogInformation("Sem mensagens a serem processadas");
            }
        }
    }
}
