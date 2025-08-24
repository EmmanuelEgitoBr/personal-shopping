using Personal.Shopping.Services.Reward.Worker;
using Personal.Shopping.Services.Reward.Worker.Services;
using Refit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Refit API (ajuste a URL da sua API)
builder.Services.AddRefitClient<IApiService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://localhost:7119"); // sua API
    });

var host = builder.Build();
host.Run();
