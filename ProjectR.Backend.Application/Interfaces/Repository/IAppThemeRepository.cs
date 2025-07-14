using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IAppThemeRepository
    {
        Task<AppThemeModel> GetByIdAsync(Guid id);
        Task<AppThemeModel[]> GetAllAsync();
    }
}
