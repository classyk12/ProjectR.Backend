using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Models.WhatsApp;

namespace ProjectR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly IWebhookManager _webhookManager;
        public WebhookController(IWebhookManager webhookManager)
        {
            _webhookManager = webhookManager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Whatsapp")]
        public async Task<IActionResult> ProcessWhatsAppMessage([FromBody] WhatsAppWebhookMessageModel model)
        {
            WebhookMessageModel webhookModel = new()
            {
                
            };

            await _webhookManager.HandleMessageAsync(webhookModel);
            return Ok();
        }
    }
}