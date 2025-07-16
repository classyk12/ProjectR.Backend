using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface IAAuthManager
    {
        //This handles user Authentication (signup and login)
        Task<ResponseModel> AuthenticateWithPhoneNumberAsync(RegistrationType);
        Task<ResponseModel> AuthenticateWithSocialAsync(RegistrationType);
    }
}
