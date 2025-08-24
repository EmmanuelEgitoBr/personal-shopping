using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Email.Application.Interfaces;
using Personal.Shopping.Services.Email.Application.Models;

namespace Personal.Shopping.Services.Email.Api.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailLoggerController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailLoggerController> _logger;

        public EmailLoggerController(IEmailService emailService, ILogger<EmailLoggerController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("send-email")]
        public async Task<ActionResult> SendEmail([FromBody] KafkaMessageEnvelope message)
        {
            try
            {
                _logger.LogInformation("Processando pedido {OrderId}", message.OrderId);

                await _emailService.SendEmailAsync(message.To, message.Subject, message.Body);

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar email: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
