using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.Repository;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class IndustryManager : IIndustryManager
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustryManager(IIndustryRepository repository)
        {
            _industryRepository = repository;
        }

        public async Task<ResponseModel<IndustryModel>> AddAsync(AddIndustryModel model)
        {
            IndustryModel entity = new()
            {
                Name = model.Name,
                Description = model.Description,
            };
            IndustryModel result = await _industryRepository.AddAsync(entity);
            return new ResponseModel<IndustryModel>(message: "Business Added Successfully", data: result, status: true);
        }

        public async Task<ResponseModel<IndustryModel>> GetByIdAsync(Guid id)
        {
            IndustryModel? result = await _industryRepository.GetByIdAsync(id);
            return new ResponseModel<IndustryModel>(message: result != null ? "Industry retrieved successfully" : "Industry not found", data: result, status: result != null);

        }

        public async Task<IndustryModel[]> GetAllAsync()
        {
            return await _industryRepository.GetAllAsync();
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            IndustryModel? existingIndustry = await _industryRepository.GetByIdAsync(id);
            if (existingIndustry == null) 
            {
                return new BaseResponseModel(message: "Industry not found", status: false);
            }

            await _industryRepository.DeleteAsync(existingIndustry);
            return new BaseResponseModel(message: "Industry succesfully deleted", status: true);
        }

      

        public async Task<ResponseModel<IndustryModel>> UpdateAsync(IndustryModel industry)
        {
            IndustryModel? existingIndustry = await _industryRepository.GetByIdAsync(industry.Id);
            if (existingIndustry == null)
            {
                return new ResponseModel<IndustryModel>(message: "Industry not found", data: default, status: false);
            }

            IndustryModel result = await _industryRepository.UpdateAsync(industry);
            return new ResponseModel<IndustryModel>(message: "Business updated successfully", data: result, status: true);
        }
    }
}
