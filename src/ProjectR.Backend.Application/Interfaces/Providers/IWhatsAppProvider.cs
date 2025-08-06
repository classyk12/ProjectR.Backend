using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Models.WhatsApp;

namespace ProjectR.Backend.Application.Interfaces.Providers
{
    public interface IWhatsAppProvider
    {
        /// <summary>
        /// Sends a plain whatsapp message with an optional link.
        /// This could be useful in promotions or just a subtle reminder
        /// </summary>
        /// <returns></returns>
        Task<bool> SendTextMessage(SimpleMessageModel model);
        /// <summary>
        /// This method will be used to push messages with predefined interactive button which takes response from users
        /// The Response by the user is then sent by the cloudApi via a webhook provided by us
        /// Use Case: When a user is prompted for an upcoming appointment. They can choose to confirm or decline or defer
        /// </summary>
        /// <returns></returns>
        Task<bool> SendInteractiveReplyButtonMessage(InteractiveMessageModel model);
    }
}
