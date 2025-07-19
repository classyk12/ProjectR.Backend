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
        Task<ResponseModel<PhoneNumberLoginResponseModel>> AuthenticateWithPhoneNumberAsync(LoginWithPhoneNumberModel model);

        /// <summary>
        /// Verifies the OTP sent to the user's phone number and completes the login process.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseModel<LoginResponseModel>> CompletePhoneNumberAuthenticationAsync(CompleteLoginWithPhoneNumberModel model);
        /// <summary>
        /// Authenticates a user using their social account (e.g., Google, Facebook) and genrates an auth token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResponseModel<LoginResponseModel>> AuthenticateWithSocialAsync(LoginWithSocialModel model);
    }
}
