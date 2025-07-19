using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Providers
{
    public interface ITwilioProvider
    {
        Task<bool> SendMessageViaWhatsAppAsync(string phoneNumber, string messageContent);
    }
}
