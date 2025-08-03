using ProjectR.Backend.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IBusinessRepository
    {
        Task<BusinessModel?> GetByIdAsync(Guid id);
        Task<BusinessModel?> GetBySlugAsync(string slug);
        Task<BusinessModel[]> GetAllAsync();
        Task<BusinessModel[]> AddAsync(BusinessModel[] businessModels);
        Task<BusinessModel> AddAsync(BusinessModel businessModel);
        Task<BusinessModel[]> UpdateAsync(BusinessModel[] businessModels);
        Task<BusinessModel> UpdateAsync(BusinessModel businessModel);
        Task DeleteAsync(BusinessModel[] businessModels);
        Task DeleteAsync(BusinessModel businessModel);
        Task<bool> SlugExistsAsync(string shortLink, Guid? excludedId = null);
    }
}
