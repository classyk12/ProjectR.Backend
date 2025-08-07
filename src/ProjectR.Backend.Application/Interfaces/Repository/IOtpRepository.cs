using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface IOtpRepository
    {
        Task<OtpModel> AddAsync(OtpModel model);
        Task<OtpModel?> GetAsync(OtpModel model);
        Task<OtpModel?> UpdateAsync(OtpModel model);
    }
}
