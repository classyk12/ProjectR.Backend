namespace ProjectR.Backend.Application.Interfaces.Providers
{
    public interface ISocialAuthProvider
    {
        Task<bool> VerifyTokenAsync(string token);
        Task<bool> GetUserInfoAsync(string token);
    }
}
