using Ocelot.Middleware;
using Personal.Shopping.GatewaySolution.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddOcelotConfig();
builder.AddSecurityConfiguration();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();



app.Run();
