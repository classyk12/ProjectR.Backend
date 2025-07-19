using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class TwilioProvider : ITwilioProvider
    {
        private readonly ILogger<SocialAuthProvider> _logger;
        private readonly TwilioSettings _settings;
        private const string WhatsAppPrefix = "whatsapp:";

        public TwilioProvider(ILogger<SocialAuthProvider> logger, IOptions<TwilioSettings> settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<bool> SendMessageViaWhatsAppAsync(string phoneNumber, string messageContent)
        {
            try
            {
                TwilioClient.Init(_settings.AccountSID, _settings.AuthToken);

                MessageResource message = await MessageResource.CreateAsync(
                    contentSid: _settings.OtpContentSid,
                    to: new Twilio.Types.PhoneNumber($"{WhatsAppPrefix}:{phoneNumber}"),
                   body: messageContent
                );

                _logger.LogInformation("Twilio Message response: {response}", JsonConvert.SerializeObject(message));

                return message != null && message.ErrorCode == null && message.Status == MessageResource.StatusEnum.Queued;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending Twilio Message to {phoneNumber}");
                return default;
            }
        }
    }
}
