using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IBusinessAvailabilityManager
    {
        Task<BusinessAvailabilityModel[]> GetByBusinessIdAsync();
        Task<ResponseModel<BusinessAvailabilityModel>> GetByIdAsync(Guid id);
        Task<ResponseModel<BusinessAvailabilityModel>> AddAsync(AddBusinessAvailabilityModel model);
        Task<ResponseModel<BusinessAvailabilityModel>> UpdateAsync(UpdateBusinessAvailabilityModel user);
        Task<BaseResponseModel?> DeleteAsync(Guid Id);
        Task<BusinessAvailabilityModel[]> GetByBusinessId(Guid businessId);
    }
}