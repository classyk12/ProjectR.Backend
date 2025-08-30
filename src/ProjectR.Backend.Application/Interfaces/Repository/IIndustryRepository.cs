using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IIndustryRepository
    {
            Task<Guid> CreateAsync(Industry industry);
            Task<Industry?> GetByIdAsync(Guid id);
            Task<List<Industry>> GetAllAsync();
            Task UpdateAsync(Industry industry);
            Task DeleteAsync(Guid id);
    }
}