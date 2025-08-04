using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IBusinessManager
    {
        Task<ResponseModel<BusinessModel>> GetByIdAsync(Guid id);
        Task<ResponseModel<BusinessModel>> GetBySlugAsync(string slug);
        Task<BusinessModel[]> GetAllAsync();
        Task<ResponseModel<BusinessModel[]>> AddAsync(AddBusinessModel[] businesses);
        Task<ResponseModel<BusinessModel>> AddAsync(AddBusinessModel business);
        Task<ResponseModel<BusinessModel>> UpdateAsync(BusinessModel business);
        Task<ResponseModel<BusinessModel[]>> UpdateAsync(BusinessModel[] businesses);
        Task<BaseResponseModel> DeleteAsync(BusinessModel[] businesses);
        Task<BaseResponseModel> DeleteAsync(Guid id);
        Task<ResponseModel<BusinessModel>> GetByUserId(Guid userId);
        Task<bool> IsBusinessExist(Guid userId);
    }
}
