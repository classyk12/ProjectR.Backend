using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IBusinessAvailabilityManager
    {
        Task<ResponseModel<BusinessAvailabilityModel>> GetByIdAsync(Guid id);
        Task<ResponseModel<BusinessAvailabilityModel>> AddAsync(AddBusinessAvailabilityModel model);
        Task<ResponseModel<BusinessAvailabilityModel>> UpdateAsync(Guid id, UpdateBusinessAvailabilityModel model);
        Task<BusinessAvailabilityModel[]> GetByBusinessId(Guid businessId, bool includeAll = false);
        /// <summary>
        /// Check if the business has any availability set already within a date range
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        Task<bool> HasActiveAvailabilityAsync(Guid businessId, DateTime startDate, DateTime endDate);
    }
}