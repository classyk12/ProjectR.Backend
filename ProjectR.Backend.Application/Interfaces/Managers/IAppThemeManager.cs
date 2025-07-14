using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IAppThemeManager
    {
        Task<ResponseModel<AppThemeModel>> GetByIdAsync(Guid id);
        Task<AppThemeModel[]> GetAllAsync();
        Task<ResponseModel<AppThemeModel[]>> AddAsync(AppThemeModel[] appThemes);
        Task<ResponseModel<AppThemeModel>> AddAsync(AppThemeModel appTheme);
        Task<ResponseModel<AppThemeModel>> UpdateAsync(AppThemeModel appTheme);
        Task<ResponseModel<AppThemeModel[]>> UpdateAsync(AppThemeModel[] appThemes);
        Task<BaseResponseModel> DeleteAsync(AppThemeModel[] appThemes);
        Task<BaseResponseModel> DeleteAsync(AppThemeModel appTheme);
    }
}
