using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IIndustryManager
    {
        Task<Guid> CreateIndustryAsync(string name, string? description);
        Task<Industry?> GetIndustryByIdAsync(Guid id);
        Task<List<Industry>> GetAllIndustriesAsync();
        Task UpdateIndustryAsync(Guid id, string name, string? description);
        Task DeleteIndustryAsync(Guid id);
    }
}
