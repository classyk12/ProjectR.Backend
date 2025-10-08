using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IIndustryRepository
    {
        Task<IndustryModel> AddAsync(IndustryModel industry);
        Task<IndustryModel?> GetByIdAsync(Guid id);
        Task<IndustryModel[]> GetAllAsync();
        Task<IndustryModel> UpdateAsync(IndustryModel industry);
        Task DeleteAsync(IndustryModel industry);
    }
}