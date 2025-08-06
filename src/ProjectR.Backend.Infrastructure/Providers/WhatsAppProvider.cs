using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Models.WhatsApp;
using ProjectR.Backend.Application.Settings;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class WhatsAppProvider : IWhatsAppProvider
    {
        private readonly ILogger<WhatsAppProvider> _logger;
        private readonly WhatsappCloudApiSettings _settings;

        public WhatsAppProvider(ILogger<WhatsAppProvider> logger, IOptions<WhatsappCloudApiSettings> settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<bool> SendInteractiveReplyButtonMessage(InteractiveMessageModel model)
        {
            if (_settings.UseMock)
            {
                return _settings.UseMock;
            }

            try
            {
                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending interactive WhatsApp Message to : {Number}", model.To);
                return default;
            }
        }

        public async Task<bool> SendTextMessage(SimpleMessageModel model)
        {
            if (_settings.UseMock)
            {
                return _settings.UseMock;
            }

            try
            {

            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending simple WhatsApp Message to : {Number}", model.To);
                return default;
            }

            return true;
        }
    }
}
