using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Settings;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ProjectR.Backend.Infrastructure.Providers
{
    public class SocialAuthProvider : ISocialAuthProvider
    {
        private readonly ILogger<SocialAuthProvider> _logger;
        private readonly GoogleSettings _googleSettings;

        public SocialAuthProvider(ILogger<SocialAuthProvider> logger, IOptions<GoogleSettings> googleSettings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _googleSettings = googleSettings.Value ?? throw new ArgumentNullException(nameof(googleSettings));
        }

        public async Task<GoogleAuthenticationVerificationModel?> VerifyGoogleTokenAsync(string token)
        {
            try
            {
                Payload payload = await ValidateAsync(token, new ValidationSettings
                {
                    Audience = new[] { _googleSettings.Android, _googleSettings.Ios }
                });

                return new GoogleAuthenticationVerificationModel
                {
                    Name = payload.FamilyName,
                    Email = payload.Email,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying Google token: {Token}", token);
                return default;
            }
        }
    }
}
