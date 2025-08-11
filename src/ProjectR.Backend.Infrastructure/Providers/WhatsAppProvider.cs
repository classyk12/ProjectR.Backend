using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Models.WhatsApp;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Shared;
using System.Text;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class WhatsAppProvider : IWhatsAppProvider
    {
        private readonly ILogger<WhatsAppProvider> _logger;
        private readonly WhatsappCloudApiSettings _settings;
        private readonly IHttpClientFactory _factory;

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
                if (model.Extras!.ContainsKey(AppConstants.MessageTypeKey))
                {
                    if (model.Extras[AppConstants.MessageTypeKey].Equals(AppConstants.SimpleMessageKey))
                    {
                        SimpleMessageModel simpleMessage = new()
                        {
                            MessagingProduct = AppConstants.MessagingProduct,
                            RecipientType = AppConstants.RecipientType,
                            To = model.Recipient,
                            Type = AppConstants.SimpleMessageType,
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

                else
                {
                   _logger.LogInformation("Unable to Determine WhatsApp Message Processing Route: No Message Type Found");
                    return false;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending interactive WhatsApp Message to : {Number}", model.Recipient);
                return default;
            }

            return true;
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
                string json = JsonConvert.SerializeObject(model);
                HttpClient client = _factory.CreateClient(AppConstants.WhatsappTag!);
                Console.WriteLine("URL: "+ client.BaseAddress+ $"{_settings.PhoneNumberId}/messages");
                Console.WriteLine(json);
                HttpResponseMessage result = await client.PostAsync($"{_settings.PhoneNumberId}/messages", new StringContent(json, Encoding.UTF8, "application/json"));
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
