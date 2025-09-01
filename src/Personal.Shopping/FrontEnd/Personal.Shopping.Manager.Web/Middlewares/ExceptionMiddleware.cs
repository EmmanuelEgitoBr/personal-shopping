using System.Net;
using System.Text.Json;

namespace Personal.Shopping.Manager.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, 
        ILogger<ExceptionMiddleware> logger, 
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // segue para o próximo middleware/controller
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado"); // log no servidor

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new { message = ex.Message, stackTrace = ex.StackTrace };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

