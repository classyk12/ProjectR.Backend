using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;

namespace ProjectR.Backend.Persistence.Repository
{
    public class WebhookMessageRepository : IWebhookMessageRepository
    {
        private readonly AppDbContext _context;
        public WebhookMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WebhookMessageModel> AddAsync(WebhookMessageModel model)
        {
            WebhookMessage entity = new() { };
            await _context.WebhookMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task DeleteAsync(WebhookMessageModel model)
        {
            WebhookMessage? entity = await _context.WebhookMessages.SingleOrDefaultAsync(c => c.Id == model.Id);
            _context.WebhookMessages.Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task<WebhookMessageModel[]> GetAllAsync()
        {
            List<WebhookMessage> entities = await _context.WebhookMessages.AsNoTracking().ToListAsync();
            return entities.Select(e => new WebhookMessageModel { }).ToArray();
        }

        public async Task<WebhookMessageModel?> GetByIdAsync(Guid id)
        {
            WebhookMessage? result = await _context.WebhookMessages.SingleOrDefaultAsync(c => c.Id == id);
            return result == null ? null : new WebhookMessageModel {  };
        }

        public async Task<WebhookMessageModel> UpdateAsync(WebhookMessageModel model)
        {
            WebhookMessage? entity = await _context.WebhookMessages.SingleOrDefaultAsync(c => c.Id == model.Id);
            _context.WebhookMessages.Update(entity!);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
