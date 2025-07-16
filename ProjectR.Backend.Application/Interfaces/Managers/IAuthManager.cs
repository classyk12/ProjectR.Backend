using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IAuthManager
    {
        /// <summary>
        /// Authenticates a user using their phone number and send a verification code to the provided phone number via whatsapp.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BaseResponseModel> AuthenticateWithPhoneNumberAsync(LoginWithPhoneNumberModel model);
        /// <summary>
        /// Authenticates a user using their social account (e.g., Google, Facebook) and genrates an auth token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<BaseResponseModel> AuthenticateWithSocialAsync(LoginWithSocialModel model);
        string GenerateAuthTokenAsync(UserModel user);
    }
}
