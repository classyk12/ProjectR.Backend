using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IWebhookMessageRepository
    {
        Task<WebhookMessageModel?> GetByIdAsync(Guid id);
        Task<WebhookMessageModel[]> GetAllAsync();
        Task<WebhookMessageModel> AddAsync(WebhookMessageModel model);
        Task<WebhookMessageModel> UpdateAsync(WebhookMessageModel model);
        Task DeleteAsync(WebhookMessageModel model);
    }
}
