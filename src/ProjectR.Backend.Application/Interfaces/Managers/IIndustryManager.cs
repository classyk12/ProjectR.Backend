using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IIndustryManager
    {
        Task<ResponseModel<IndustryModel>> AddAsync(AddIndustryModel industry);
        Task<ResponseModel<IndustryModel>> GetByIdAsync(Guid id);
        Task<IndustryModel[]> GetAllAsync();
        Task<ResponseModel<IndustryModel>> UpdateAsync(IndustryModel industry);
        Task<BaseResponseModel> DeleteAsync(Guid id);
    }
}
