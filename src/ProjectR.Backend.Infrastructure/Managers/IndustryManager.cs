using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class IndustryManager : IIndustryManager
    {
        private readonly IIndustryRepository _repository;

        public IndustryManager(IIndustryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateIndustryAsync(string name, string? description)
        {
            var industry = new Industry
            { 
                Id = Guid.NewGuid(), 
                Name = name,
                Description = description
            };
            return await _repository.CreateAsync(industry);
        }

        public async Task<Industry?> GetIndustryByIdAsync(Guid id)
        {
            var industry = await _repository.GetByIdAsync(id);
            return industry == null ? null : new Industry
            { 
                Id = industry.Id, 
                Name = industry.Name ,
                Description = industry.Description
            };
        }

        public async Task<List<Industry>> GetAllIndustriesAsync()
        {
            var industries = await _repository.GetAllAsync();
            return industries.Select(i => new Industry 
            { 
                Id = i.Id, 
                Name = i.Name, 
                Description = i.Description
            }).ToList();
        }

        public async Task UpdateIndustryAsync(Guid id, string name, string? description)
        {
            var industry = await _repository.GetByIdAsync(id);
            if (industry != null)
            {
                industry.Name = name;
                industry.Description = description;
                await _repository.UpdateAsync(industry);
            }
        }

        public async Task DeleteIndustryAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
