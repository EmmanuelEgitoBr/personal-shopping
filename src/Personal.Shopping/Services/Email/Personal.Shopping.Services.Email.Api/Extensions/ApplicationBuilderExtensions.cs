using Personal.Shopping.Services.Email.Application.Interfaces;

namespace Personal.Shopping.Services.Email.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    private static IServiceBusConsumer? ServiceBusConsumer { get; set; }

    public static IApplicationBuilder UseSqsServiceBusConsumer(this IApplicationBuilder app)
    {
        ServiceBusConsumer = app.ApplicationServices.GetService<IServiceBusConsumer>()!;
        var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        hostApplicationLifetime!.ApplicationStarted.Register(OnStart);
        hostApplicationLifetime!.ApplicationStopping.Register(OnStop);

        return app;
    }

    private static void OnStop()
    {
        ServiceBusConsumer!.StopAsync();
    }

    private static void OnStart()
    {
        ServiceBusConsumer!.StartAsync(CancellationToken.None);
    }
}
