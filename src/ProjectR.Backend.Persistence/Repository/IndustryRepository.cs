using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Persistence.DatabaseContext;

namespace ProjectR.Backend.Persistence.Repository
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly AppDbContext _appDbContext;

        public IndustryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> CreateAsync(Industry industry)
        {
            await _appDbContext.Industries.AddAsync(industry);
            await _appDbContext.SaveChangesAsync();
            return industry.Id;
        }

        public async Task<Industry?> GetByIdAsync(Guid id) => await _appDbContext.Industries.FindAsync(id);

        public async Task<List<Industry>> GetAllAsync() => await _appDbContext.Industries.ToListAsync();

        public async Task UpdateAsync(Industry industry)
        {
            _appDbContext.Industries.Update(industry);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var industry = await _appDbContext.Industries.FindAsync(id);
            if (industry != null)
            {
                _appDbContext.Industries.Remove(industry);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
