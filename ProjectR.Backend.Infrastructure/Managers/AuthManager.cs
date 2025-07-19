using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ProjectR.Backend.Application.Settings;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtSettings _options;
        private readonly ISocialAuthProvider _socialAuthProvider;
        private readonly INotificationManager _notificationManager;

        public AuthManager(IOptions<JwtSettings> options, ISocialAuthProvider socialAuthProvider, INotificationManager notificationManager)
        {
            _notificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));
            _socialAuthProvider = socialAuthProvider ?? throw new ArgumentNullException(nameof(socialAuthProvider));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<ResponseModel<PhoneNumberLoginResponseModel>> AuthenticateWithPhoneNumberAsync(LoginWithPhoneNumberModel model)
        {
            //validate phone number and phone code
            //if valid, insert and create a new OTP instance in the database
            // generate an OTP token and send it to the user's phone number via whatsapp

            //TODO: notification manager is to be replaced by OTP manager 
            NotificationModel notificationModel = new([DeliveryMode.Whatsapp], model.PhoneNumber);
            BaseResponseModel sendNotificationReult = await _notificationManager.SendNotificationAsync(notificationModel);
            if (!sendNotificationReult.Status)
            {
                return new ResponseModel<PhoneNumberLoginResponseModel>("Failed to send OTP.", default, false);
            }

            return new ResponseModel<PhoneNumberLoginResponseModel>("OTP sent successfully.", new PhoneNumberLoginResponseModel
            {
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                OtpToken = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber
            }, true);
        }

        public async Task<ResponseModel<LoginResponseModel>> AuthenticateWithSocialAsync(LoginWithSocialModel model)
        {
            //check if the email of the uuser in the database
            //if not, validate the social token
            GoogleAuthenticationVerificationModel? verificationResult = await _socialAuthProvider.VerifyGoogleTokenAsync(model.Token!);
            if (verificationResult == null)
            {
                return new ResponseModel<LoginResponseModel>("Invalid Social Authentication Token.", default, false);
            }

            //if valid, create a new user in the database

            //if exist already, generate a new auth token
            string generatedToken = GenerateAuthTokenAsync(new UserModel
            {

            });

            return new ResponseModel<LoginResponseModel>("User authenticated successfully.", new LoginResponseModel
            {
                AuthToken = generatedToken,
                User = new UserModel
                {

                }
            }, true);
        }

        private string GenerateAuthTokenAsync(UserModel user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_options.Key);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email ?? string.Empty),
                    new(ClaimTypes.MobilePhone, user.PhoneCode + user.PhoneNumber ?? string.Empty),
                    new("AccountType", user.AccountType.ToString()!),
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _options.Issuer,
                Audience = _options.Audience
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
