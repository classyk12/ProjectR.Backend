using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Shared.Mappers;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class BusinessAvailabilityManager : IBusinessAvailabilityManager
    {
        public readonly IBusinessAvailabilityRepository _repository;
        public readonly IBusinessManager _businessManager;
        private readonly BusinessAvailabilitySettings _options;
        private readonly IValidator<AddBusinessAvailabilityModel> _validator;

        public BusinessAvailabilityManager(IBusinessAvailabilityRepository repository, IOptions<BusinessAvailabilitySettings> options, IValidator<AddBusinessAvailabilityModel> validator, IBusinessManager businessManager)
        {
            _repository = repository;
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _businessManager = businessManager ?? throw new ArgumentNullException(nameof(businessManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> AddAsync(AddBusinessAvailabilityModel model)
        {
            ResponseModel<BusinessModel> business = await _businessManager.GetByIdAsync(model.BusinessId);
            if (business.Data == null)
            {
                return new ResponseModel<BusinessAvailabilityModel>("Business not found", null, false);
            }

            //TODO: check if the business already has availability set for the same day and time range
            //for example, if the business has availability on 12th - 19th June, it should not be able to add another availability for the same date range

            ValidationResult validate = _validator.Validate(model);
            if (!validate.IsValid)
            {
                return new ResponseModel<BusinessAvailabilityModel>(message: "Validation errors occurred", data: null, status: false, errors: validate.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            BusinessAvailabilityModel newAvailability = Mapper.Map<AddBusinessAvailabilityModel, BusinessAvailabilityModel>(model);
            await _repository.AddAsync(newAvailability);
            return new ResponseModel<BusinessAvailabilityModel>(message: "Business Availability created successfully", data: newAvailability, status: true);
        }

        public async Task<BusinessAvailabilityModel[]> GetByBusinessId(Guid businessId)
        {
            BusinessAvailabilityModel[] result = await _repository.GetAllByBusinessIdAsync(businessId);
            return result;
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> GetByIdAsync(Guid id)
        {
            BusinessAvailabilityModel? result = await _repository.GetByIdAsync(id);
            return new ResponseModel<BusinessAvailabilityModel>(message: result != null ? "Business Availability retrieved successfully" : "Business Availability not found", data: result, status: result != null);
        }

        public async Task<bool> HasActiveAvailabilityAsync(Guid businessId, DateOnly startDate, DateOnly endDate)
        {
            return await _repository.HasActiveAvailabilityAsync(businessId, startDate, endDate);
        }

        public async Task<ResponseModel<BusinessAvailabilityModel>> UpdateAsync(Guid id, UpdateBusinessAvailabilityModel model)
        {
            BusinessAvailabilityModel? existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return new ResponseModel<BusinessAvailabilityModel>("Business Availability not found", null, false);
            }

            UpdateBusinessAvailabilityModel updatedAvailability = Mapper.Map<UpdateBusinessAvailabilityModel, UpdateBusinessAvailabilityModel>(model);
            BusinessAvailabilityModel result = await _repository.UpdateAsync(id, updatedAvailability);

            //put all notification into a table and have a background service send them out or Send them out in parallel using Task.WhenAll
            //TODO: send notification to business owner about the update (via WhatsApp)
            //TODO: send notification to customers who have bookings in the updated slots (WhatsApp)
            //TODO: send out a SignalR notification to connected clients about the update
            return new ResponseModel<BusinessAvailabilityModel>("Business Availability updated successfully", result, true);
        }
    }
}
