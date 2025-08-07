using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IAppThemeRepository
    {
        Task<AppThemeModel?> GetByIdAsync(Guid id);
        Task<AppThemeModel[]> GetAllAsync();
        Task<AppThemeModel[]> AddAsync(AppThemeModel[] appThemes);
        Task<AppThemeModel> AddAsync(AppThemeModel appTheme);
        Task<AppThemeModel> UpdateAsync(AppThemeModel appTheme);
        Task<AppThemeModel[]> UpdateAsync(AppThemeModel[] appThemes);
        Task DeleteAsync(AppThemeModel[] appThemes);
        Task DeleteAsync(AppThemeModel appTheme);
    }
}
