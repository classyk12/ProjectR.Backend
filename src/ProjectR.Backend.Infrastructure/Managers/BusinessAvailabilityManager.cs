using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class BusinessAvailabilityManager : IBusinessAvailabilityManager
    {
        public readonly IBusinessAvailabilityRepository _repository;

        public BusinessAvailabilityManager(IBusinessAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> AddAsync(AddBusinessAvailabilityModel model)
        {
            //check if the days are more than one week apart
            //check if the business exist
            //check if business availability already exists for the business

            //check that slots are valid i.e One slot to One day of the week
            //check that only one date is used in the slots
            //check that there are not dup

            //ensure that start time of the date is before the end time
            //ensure that the times in the breaks are within the time of the slot
            //should we let users have breaks occupying the whole slot time?

            // Map to BusinessAvailabilityModel

            // Create new availability

            BusinessAvailabilityModel newAvailability = new()
            {
                BusinessId = model.BusinessId,
                Slots = model.Slots?.Select(slot => new BusinessAvailabilitySlotModel
                {
                    Date = slot.Date,
                    DayOfWeek = slot.Date.DayOfWeek,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime,
                    Breaks = model.Breaks?.Select(b => new BreakModel
                    {
                        StartTime = b.StartTime,
                        EndTime = b.EndTime
                    }).ToList()
                }).ToList()
            };

            await _repository.AddAsync(newAvailability);
            return new ResponseModel<BusinessAvailabilityModel>(message: "Business Availability created successfully", data: newAvailability, status: true);
        }

        public async Task<BaseResponseModel?> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessAvailabilityModel[]> GetByBusinessId(Guid businessId)
        {
            BusinessAvailabilityModel[] result = await _repository.GetAllByBusinessIdAsync(businessId);
            return result;
        }

        public async Task<BusinessAvailabilityModel[]> GetByBusinessIdAsync()
        {
            BusinessAvailabilityModel[] result = await _repository.GetAllByBusinessIdAsync(Guid.Empty);
            return result;
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> GetByIdAsync(Guid id)
        {
            BusinessAvailabilityModel? result = await _repository.GetByIdAsync(id);
            return new ResponseModel<BusinessAvailabilityModel>(message: result != null ? "Business Availability retrieved successfully" : "Business Availability not found", data: result, status: result != null);
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> UpdateAsync(UpdateBusinessAvailabilityModel user)
        {
            throw new NotImplementedException();
        }
    }
}
