using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;

namespace ProjectR.Backend.Persistence.Repository
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly AppDbContext _context;

        public IndustryRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IndustryModel> AddAsync(IndustryModel industryModel)
        {
            Industry entity = new()
            {
                Name = industryModel.Name,
                Description = industryModel.Description
            };

            EntityEntry<Industry> result = await _context.Industries.AddAsync(entity);
            result.Entity.Id = entity.Id;
            await _context.SaveChangesAsync();
            return industryModel;
            
        }

        public async Task<IndustryModel?> GetByIdAsync(Guid id)
        {
            Industry? entity = await _context.Industries.SingleOrDefaultAsync(i => i.Id.Equals(id));
            return entity == null ? null : new IndustryModel
            {
                Id = entity.Id,
                Name = entity?.Name,
                Description = entity?.Description,
            };
            
        }

        public async Task<IndustryModel[]> GetAllAsync()
        {
            List<Industry> entities = await _context.Industries.ToListAsync();
            return entities.Select(i => new IndustryModel
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
            }).ToArray();
        }

        public async Task<IndustryModel> UpdateAsync(IndustryModel industry)
        {
            Industry? entity = await _context.Industries.SingleOrDefaultAsync(i => i.Id.Equals(industry.Id));
            entity!.Name = industry.Name;
            entity!.Description = industry.Description;
            _context.Industries.Update(entity);
            await _context.SaveChangesAsync();
            return industry;
        }

        public async Task DeleteAsync(IndustryModel model)
        {
            Industry? industry = await _context.Industries.FindAsync(model.Id);
            if (industry != null)
            {
                _context.Industries.Remove(industry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
