using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IBusinessAvailabilityRepository
    {
        Task<BusinessAvailabilityModel?> GetByIdAsync(Guid id);
        Task<BusinessAvailabilityModel[]> GetAllByBusinessIdAsync(Guid id);
        Task<BusinessAvailabilityModel> AddAsync(BusinessAvailabilityModel model);
        Task<BusinessAvailabilityModel> UpdateAsync(Guid id, UpdateBusinessAvailabilityModel model);
    }
}