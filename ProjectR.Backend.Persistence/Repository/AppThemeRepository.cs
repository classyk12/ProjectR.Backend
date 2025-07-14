using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;

namespace ProjectR.Backend.Persistence.Repository
{
    public class AppThemeRepository : IAppThemeRepository
    {
        private readonly AppDbContext _context;
        public AppThemeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppThemeModel[]> AddAsync(AppThemeModel[] appThemes)
        {
            List<AppTheme> entities = appThemes.Select(c => new AppTheme { Id = c.Id }).ToList();
            await _context.AppThemes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return appThemes;
        }

        public async Task<AppThemeModel> AddAsync(AppThemeModel appTheme)
        {
            AppTheme entity = new() { Id = appTheme.Id };
            await _context.AppThemes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return appTheme;
        }

        public async Task DeleteAsync(AppThemeModel[] appThemes)
        {
            List<AppTheme> entities = appThemes.Select(c => new AppTheme { Id = c.Id }).ToList();
            _context.AppThemes.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AppThemeModel appTheme)
        {
            AppTheme? entity = await _context.AppThemes.SingleOrDefaultAsync(c => c.Id == appTheme.Id);
            _context.AppThemes.Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task<AppThemeModel[]> GetAllAsync()
        {
            List<AppTheme> entities = await _context.AppThemes.ToListAsync();
            return entities.Select(e => new AppThemeModel { Id = e.Id }).ToArray();
        }

        public async Task<AppThemeModel> GetByIdAsync(Guid id)
        {
            await _context.AppThemes.SingleOrDefaultAsync(c => c.Id == id);
            return new AppThemeModel { Id = id };
        }

        public async Task<AppThemeModel> UpdateAsync(AppThemeModel appTheme)
        {
            AppTheme? entity = await _context.AppThemes.SingleOrDefaultAsync(c => c.Id == appTheme.Id);
            _context.AppThemes.Update(entity!);
            await _context.SaveChangesAsync();
            return appTheme;
        }

        public async Task<AppThemeModel[]> UpdateAsync(AppThemeModel[] appThemes)
        {
            List<AppTheme> entities = appThemes.Select(c => new AppTheme { Id = c.Id }).ToList();
            _context.AppThemes.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return appThemes;
        }
    }
}
