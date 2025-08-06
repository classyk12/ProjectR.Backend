using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IOtpManager
    {
        Task<ResponseModel<OtpModel>> AddAsync(OtpModel model);
        Task<ResponseModel<OtpModel>> VerifyAsync(OtpModel model);
    }
}
