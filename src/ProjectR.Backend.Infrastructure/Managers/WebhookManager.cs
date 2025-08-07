using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class WebhookManager : IWebhookManager
    {
        private readonly IWebhookMessageRepository _repository;

        public WebhookManager(IWebhookMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleMessageAsync(WebhookMessageModel model)
        {
            await _repository.AddAsync(model);
        }
    }
}
