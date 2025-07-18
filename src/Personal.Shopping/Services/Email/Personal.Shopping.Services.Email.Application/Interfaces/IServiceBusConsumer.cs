namespace Personal.Shopping.Services.Email.Application.Interfaces;

public interface IServiceBusConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync();
}
