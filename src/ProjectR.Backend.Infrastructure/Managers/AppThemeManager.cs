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
            AppThemeModel? result = await _appThemeRepository.GetByIdAsync(id);
            return new ResponseModel<AppThemeModel>(
                message: result != null ? "App theme retrieved successfully." : "App theme not found.",
                data: result,
                status: result != null);
        }

        public async Task<AppThemeModel[]> GetAllAsync()
        {
            return await _appThemeRepository.GetAllAsync();
        }

        public async Task<ResponseModel<AppThemeModel[]>> AddAsync(AddAppThemeModel[] appThemes)
        {
            if (appThemes.Length == 0)
            {
                return new ResponseModel<AppThemeModel[]>(message: "No app themes provided to add.", status: false, data: Array.Empty<AppThemeModel>());
            }

            //TODO: Validate appThemes before adding
            //TODO: check for duplicates 
            AppThemeModel[] models = appThemes.Select(at => new AppThemeModel { Name = at.Name }).ToArray();
            AppThemeModel[] result = await _appThemeRepository.AddAsync(models);
            return new ResponseModel<AppThemeModel[]>(message: "App themes added successfully.", data: result, status: true);
        }

        public async Task<ResponseModel<AppThemeModel>> AddAsync(AddAppThemeModel appTheme)
        {
            AppThemeModel model = new AppThemeModel { Name = appTheme.Name };
            AppThemeModel result = await _appThemeRepository.AddAsync(model);
            return new ResponseModel<AppThemeModel>(message: "App theme added successfully.", data: result, status: true);
        }

        public async Task<ResponseModel<AppThemeModel>> UpdateAsync(AppThemeModel appTheme)
        {
            AppThemeModel? existingTheme = await _appThemeRepository.GetByIdAsync(appTheme.Id);

            if (existingTheme == null)
            {
                return new ResponseModel<AppThemeModel>(message: "App theme not found.", data: default, status: false);
            }

            AppThemeModel result = await _appThemeRepository.UpdateAsync(appTheme);
            return new ResponseModel<AppThemeModel>(message: "App theme updated successfully.", data: result, status: true);
        }

        public async Task<ResponseModel<AppThemeModel[]>> UpdateAsync(AppThemeModel[] appThemes)
        {
            if (appThemes.Length == 0)
            {
                return new ResponseModel<AppThemeModel[]>(message: "No app themes provided to add.", status: false, data: Array.Empty<AppThemeModel>());
            }

            //TODO: Validate appThemes before adding
            //TODO: check for duplicates 

            AppThemeModel[] result = await _appThemeRepository.UpdateAsync(appThemes);
            return new ResponseModel<AppThemeModel[]>(message: "App themes updated successfully.", data: result, status: true);
        }

        public async Task<BaseResponseModel> DeleteAsync(AppThemeModel[] appThemes)
        {
            await _appThemeRepository.DeleteAsync(appThemes);
            return new BaseResponseModel(message: "App theme updated successfully.", status: true);
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid Id)
        {
           AppThemeModel? existingTheme = await _appThemeRepository.GetByIdAsync(Id);

            if (existingTheme == null)
            {
                return new BaseResponseModel(message: "App theme not found.", status: false);
            }

            await _appThemeRepository.DeleteAsync(existingTheme);
            return new BaseResponseModel(message: "App theme deleted successfully.", status: true);
        }
    }
}
