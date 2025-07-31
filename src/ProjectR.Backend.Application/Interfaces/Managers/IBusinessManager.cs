using ProjectR.Backend.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IBusinessManager
    {
        Task<ResponseModel<BusinessModel>> GetByIdAsync(Guid id);
        Task<BusinessModel[]> GetAllAsync();
        Task<ResponseModel<BusinessModel[]>> AddAsync(AddBusinessModel[] businesses);
        Task<ResponseModel<BusinessModel>> AddAsync(AddBusinessModel business);
        Task<ResponseModel<BusinessModel>> UpdateAsync(BusinessModel business);
        Task<ResponseModel<BusinessModel[]>> UpdateAsync(BusinessModel[] businesses);
        Task<BaseResponseModel> DeleteAsync(BusinessModel[] businesses);
        Task<BaseResponseModel> DeleteAsync(Guid id);

    }
}
