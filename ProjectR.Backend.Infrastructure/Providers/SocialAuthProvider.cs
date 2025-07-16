using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class SocialAuthProvider : ISocialAuthProvider
    {
        public Task<bool> GetUserInfoAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
