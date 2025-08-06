using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Models.WhatsApp;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class WhatsAppProvider : IWhatsAppProvider
    {
        private readonly ILogger<WhatsAppProvider> _logger;
        private readonly WhatsappCloudApiSettings _settings;
        private readonly IHttpClientFactory _factory;
        private readonly string MessageTypeKey = "Type";
        private readonly string SimpleMessageKey = "SimpleMessage";
        private readonly string InteractiveMessageKey = "InteractiveMessage";
        private readonly string MessagingProduct = "whatsapp";
        private readonly string RecipientType = "individual";
        private readonly string SimpleMessageType = "text";
        private readonly string InteractiveMessageType = "interactive";

        public WhatsAppProvider(ILogger<WhatsAppProvider> logger, IOptions<WhatsappCloudApiSettings> settings, IHttpClientFactory factory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
            _factory = factory;
        }

        public async Task<bool> SendMessageAsync(NotificationModel model)
        {
            //check for the type of message (e.g Simple or Interactive from the Extras)
            //use IF/ELSE to redirect call based on message type
            try
            {
                if (model.Extras!.ContainsKey(MessageTypeKey))
                {
                    if (model.Extras[MessageTypeKey].Equals(SimpleMessageKey))
                    {
                        SimpleMessageModel simpleMessage = new()
                        {
                            MessagingProduct = MessagingProduct,
                            RecipientType = RecipientType,
                            To = model.Recipient,
                            Type = SimpleMessageType,
                            Text = new Text
                            {
                                Body = model.Content
                            }
                        };

                        await SendSimpleMessageAsync(simpleMessage);
                    }

                    else
                    {
                        InteractiveMessageModel interactiveMessageModel = new()
                        {

                        };

                        await SendInteractiveReplyButtonMessageAsync(interactiveMessageModel);
                    }
                }

                _logger.LogInformation("Unable to Determine WhatsApp Message Processing Route: No Message Type Found");
                return false;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending interactive WhatsApp Message to : {Number}", model.Recipient);
                return default;
            }

            throw new NotImplementedException();
        }

        public async Task<bool> SendInteractiveReplyButtonMessageAsync(InteractiveMessageModel model)
        {
            if (_settings.UseMock)
            {
                return _settings.UseMock;
            }

            try
            {
                HttpClient client = _factory.CreateClient(AppConstants.WhatsappTag!);
                HttpResponseMessage result = await client.GetAsync($"{_settings.PhoneNumberId}/messages");
                string response = await result.Content.ReadAsStringAsync();
                _logger.LogInformation("result to {Url} is {Result}", client.BaseAddress, response);
                return result.IsSuccessStatusCode;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending interactive WhatsApp Message to : {Number}", model.To);
                return default;
            }
        }
        public async Task<bool> SendSimpleMessageAsync(SimpleMessageModel model)
        {
            if (_settings.UseMock)
            {
                return _settings.UseMock;
            }

            try
            {
                HttpClient client = _factory.CreateClient(AppConstants.WhatsappTag!);
                HttpResponseMessage result = await client.GetAsync($"{_settings.PhoneNumberId}/messages");
                string response = await result.Content.ReadAsStringAsync();
                _logger.LogInformation("result to {Url} is {Result}", client.BaseAddress, response);
                return result.IsSuccessStatusCode;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending simple WhatsApp Message to : {Number}", model.To);
                return default;
            }
        }
    }
}
