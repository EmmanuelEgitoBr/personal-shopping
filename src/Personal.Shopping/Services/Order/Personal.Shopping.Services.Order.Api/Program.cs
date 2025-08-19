using Personal.Shopping.Services.Order.Api.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddSqlConfiguration();
builder.AddApplicationConfig();
builder.AddMapperConfiguration();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

StripeConfiguration.ApiKey = "sk_test_51RwRjbRvsAANkPBgaLkTObdUYCtfCKAPEuZdeQVmsnBM4XAZ3nRgh9u2hxG8cD5ym6QxQziiPkwUIF3JRkODg9KJ00xJLQmXMP";
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
