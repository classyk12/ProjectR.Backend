using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Providers
{
    public interface ISocialAuthProvider
    {
        Task<GoogleAuthenticationVerificationModel?> VerifyGoogleTokenAsync(string token);
    }
}
