using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class BusinessManager : IBusinessManager
    {
        public readonly IBusinessRepository _businessRepository;

        public BusinessManager(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }
      
        public async Task<ResponseModel<BusinessModel>> AddAsync(AddBusinessModel business)
        {
             if (!double.TryParse(business.Latitude?.ToString(), out double latitude) || latitude < -90 || latitude > 90)
            {
                return new ResponseModel<BusinessModel>(message: "Latitude must be a number between -90 and 90.", status: true, data: default);
            }

            if (!double.TryParse(business.Longitude?.ToString(), out double longitude) || longitude < -180 || longitude > 180)
            {
                return new ResponseModel<BusinessModel>(message: "Longitude must be a number between -180 and 180.", status: true, data: default);
            }

            BusinessModel model = new()
            {
                UserId = business.UserId,
                Name = business.Name,
                Type = business.Type,
                PhoneCode = business.PhoneCode,
                PhoneNumber = business.PhoneNumber,
                Industry = business.Industry,
                About = business.About,
                Location = business.Location,
                Longitude = business.Longitude,
                Latitude = business.Latitude,
            };

            BusinessModel result = await _businessRepository.AddAsync(model);
            return new ResponseModel<BusinessModel>(message: "Business Added Successfully", data: result, status: true);
        }

        public async Task<ResponseModel<BusinessModel[]>> AddAsync(AddBusinessModel[] businesses)
        {
            if (businesses.Length == 0)
            {
                return new ResponseModel<BusinessModel[]>(message: "Empty Business", status: false, data: Array.Empty<BusinessModel>());
            }

             foreach (AddBusinessModel b in businesses)
            {
                if (!double.TryParse(b.Latitude?.ToString(), out double latitude) || latitude < -90 || latitude > 90)
                {
                    return new ResponseModel<BusinessModel[]>(message: $"Latitude for {b.Name} must be a number between -90 and 90.", status: true, data: Array.Empty<BusinessModel>());
                }

                if (!double.TryParse(b.Longitude?.ToString(), out double longitude) || longitude < -180 || longitude > 180)
                {
                    return new ResponseModel<BusinessModel[]>(message: $"Longitude for {b.Name} must be a number between -180 and 180.", status: true, data: Array.Empty<BusinessModel>());
                }
            }

            BusinessModel[] models = businesses.Select(at => new BusinessModel
            {
                Name = at.Name,
                Type = at.Type,
                PhoneCode = at.PhoneCode,
                PhoneNumber = at.PhoneNumber,
                Industry = at.Industry, 
                About = at.About,
            }).ToArray();

            BusinessModel[] result = await _businessRepository.AddAsync(models);
            return new ResponseModel<BusinessModel[]>(message: "Business Added Successfully", data: result, status: true);

        }

        public async Task<BaseResponseModel> DeleteAsync(BusinessModel[] businesses)
        {
            await _businessRepository.DeleteAsync(businesses);
            return new BaseResponseModel(message: "Business succesfully deleted", status: true);
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            BusinessModel exisitingBusiness = await _businessRepository.GetByIdAsync(id);
            if (exisitingBusiness == null)
            {
                return new BaseResponseModel(message: "Business not found", status: false);
            }

            await _businessRepository.DeleteAsync(exisitingBusiness);
            return new BaseResponseModel(message:"Business succesfully deleted", status: true);
        }

        public async Task<BusinessModel[]> GetAllAsync()
        {
            return await _businessRepository.GetAllAsync();
        }

        public async Task<ResponseModel<BusinessModel>> GetByIdAsync(Guid id)
        {
            BusinessModel result = await _businessRepository.GetByIdAsync(id);
            return new ResponseModel<BusinessModel>(message: result != null ? "Business retrieved successfully" : "Business not found",result, result != null);
        }

        public async Task<ResponseModel<BusinessModel>> UpdateAsync(BusinessModel business)
        {
             if (!double.TryParse(business.Latitude?.ToString(), out double latitude) || latitude < -90 || latitude > 90)
            {
                return new ResponseModel<BusinessModel>(message: "Latitude must be a number between -90 and 90.", status: true, data: default);
            }

            if (!double.TryParse(business.Longitude?.ToString(), out double longitude) || longitude < -180 || longitude > 180)
            {
                return new ResponseModel<BusinessModel>(message: "Longitude must be a number between -180 and 180.", status: true, data: default);
            }
            
            BusinessModel existingBusiness = await _businessRepository.GetByIdAsync(business.Id);
            if (existingBusiness == null)
            {
                return new ResponseModel<BusinessModel>(message: "Business not found", data: default, status: false);
            }

            BusinessModel result = await _businessRepository.UpdateAsync(business);
            return new ResponseModel<BusinessModel>(message: "Business updated successfully", data: result, status: true);
        }

        public async Task<ResponseModel<BusinessModel[]>> UpdateAsync(BusinessModel[] businesses)
        {
            if (businesses.Length == 0)
            {
                return new ResponseModel<BusinessModel[]>(message: "Empty Business", status: false, data: Array.Empty<BusinessModel>());
            }
           
            BusinessModel[] result = await _businessRepository.UpdateAsync(businesses);
            return new ResponseModel<BusinessModel[]>(message: "Business updated successfully", data: result , status: true);

        }
    }
}
