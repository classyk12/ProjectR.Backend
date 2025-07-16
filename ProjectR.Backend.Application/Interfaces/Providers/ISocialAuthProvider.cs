using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Repository
{
    public interface ISocialAuthProvider
    {
        Task<bool> VerifyTokenAsync(string token);
        Task<bool> GetUserInfoAsync(string token);
    }
}
