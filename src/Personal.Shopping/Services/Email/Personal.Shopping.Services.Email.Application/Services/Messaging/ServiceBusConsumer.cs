using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Personal.Shopping.Services.Email.Application.Dtos.Cart;
using Personal.Shopping.Services.Email.Application.Interfaces;

namespace Personal.Shopping.Services.Email.Application.Services.Messaging;

public class ServiceBusConsumer : IServiceBusConsumer
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IConfiguration _configuration;
    private readonly string _queueUrl;
    private CancellationTokenSource _cts;
    private Task? _pollingTask;

    public ServiceBusConsumer(IAmazonSQS sqsClient, IConfiguration configuration)
    {
        _sqsClient = sqsClient;
        _configuration = configuration;
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

            foreach (var message in response.Messages)
            {
                try
                {
                    // Deserialize e processa a mensagem
                    var cart = JsonConvert.DeserializeObject<CartDto>(message.Body);

                    // TODO: Processar email
                    Console.WriteLine($"Recebido: {message.Body}");

                    // Apagar da fila após processar
                    await _sqsClient.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                    // opcional: não deletar a mensagem para retry automático
                }
            }
        }
    }
}
