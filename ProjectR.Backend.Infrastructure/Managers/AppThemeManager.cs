using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class AppThemeManager : IAppThemeManager
    {
        private readonly IAppThemeRepository _appThemeRepository;

        public AppThemeManager(IAppThemeRepository appThemeRepository)
        {
            _appThemeRepository = appThemeRepository;
        }

        public async Task<ResponseModel<AppThemeModel>> GetByIdAsync(Guid id)
        {
            AppThemeModel result = await _appThemeRepository.GetByIdAsync(id);
            return new ResponseModel<AppThemeModel>(message: result != null ? "App theme retrieved successfully." : "App theme not found.", result, result != null);
        }

        public async Task<AppThemeModel[]> GetAllAsync()
        {
            return await _appThemeRepository.GetAllAsync();
        }

        public async Task<ResponseModel<AppThemeModel[]>> AddAsync(AppThemeModel[] appThemes)
        {
            if (appThemes.Length == 0)
            {
                return new ResponseModel<AppThemeModel[]>(message: "No app themes provided to add.", status: false, data: Array.Empty<AppThemeModel>());
            }

            return await _appThemeRepository.AddAsync(appThemes);
        }

        public async Task<AppThemeModel> AddAsync(AppThemeModel appTheme)
        {
            return await _appThemeRepository.AddAsync(appTheme);
        }

        public async Task<AppThemeModel> UpdateAsync(AppThemeModel appTheme)
        {
            return await _appThemeRepository.UpdateAsync(appTheme);
        }

        public async Task<AppThemeModel[]> UpdateAsync(AppThemeModel[] appThemes)
        {
            return await _appThemeRepository.UpdateAsync(appThemes);
        }

        public async Task DeleteAsync(AppThemeModel[] appThemes)
        {
            await _appThemeRepository.DeleteAsync(appThemes);
        }

        public async Task DeleteAsync(AppThemeModel appTheme)
        {
            await _appThemeRepository.DeleteAsync(appTheme);
        }
    }
}
