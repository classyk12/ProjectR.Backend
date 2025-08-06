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
