using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IWebhookManager
    {
        /// <summary>
        /// This handles all webhooks requests.
        /// </summary>
        /// <returns></returns>
        Task HandleMessageAsync();
    }
}

//Webhook table
//payload => serialized string
// Source => whatsapp, twilio etc
//IsProcessed => true, false
//LastProcessedResult => result of the processing <log error if it fails>
//LastProcessedDate
