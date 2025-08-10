using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ProjectR.Backend.Application.Settings;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly JwtSettings _options;
        private readonly ISocialAuthProvider _socialAuthProvider;
        private readonly INotificationManager _notificationManager;
        private readonly IOtpManager _otpManager;
        private readonly IUserManager _userManager;
        private readonly IBusinessManager _businessManager;

        public AuthManager(IOptions<JwtSettings> options, ISocialAuthProvider socialAuthProvider, INotificationManager notificationManager, IOtpManager otpManager, IUserManager userManager,
        IBusinessManager businessManager
        )
        {
            _notificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));
            _socialAuthProvider = socialAuthProvider ?? throw new ArgumentNullException(nameof(socialAuthProvider));
            _otpManager = otpManager ?? throw new ArgumentNullException(nameof(otpManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _businessManager = businessManager ?? throw new ArgumentNullException(nameof(businessManager));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<ResponseModel<PhoneNumberLoginResponseModel>> AuthenticateWithPhoneNumberAsync(LoginWithPhoneNumberModel model)
        {
            OtpModel otpModel = new()
            {
                PhoneNumber = model.PhoneNumber,
                CountryCode = model.PhoneCode,
                DeliveryMode = DeliveryMode.Whatsapp,
                OtpType = OtpType.Authentication
            };

            ResponseModel<OtpModel> otp = await _otpManager.AddAsync(otpModel);
            if (!otp.Status)
            {
                return new ResponseModel<PhoneNumberLoginResponseModel>("Failed to send OTP. Try again", default, false);
            }

            Dictionary<string, object> extras = new()
            {
                { AppConstants.MessageTypeKey, AppConstants.SimpleMessageKey },
            };

            NotificationModel notificationModel = new([DeliveryMode.Whatsapp], model.PhoneCode + model.PhoneNumber, $"Your OTP Code is {otp.Data?.Code}", extras);
            await _notificationManager.SendNotificationAsync(notificationModel);

            return new ResponseModel<PhoneNumberLoginResponseModel>("OTP sent successfully.", new PhoneNumberLoginResponseModel
            {
                ExpiresAt = otpModel.ExpiryDate,
                OtpToken = otp.Data?.Id,
                Type = otpModel.OtpType,
                PhoneNumber = model.PhoneNumber,
                PhoneCode = model.PhoneCode
            }, true);
        }

        public async Task<ResponseModel<LoginResponseModel>> CompletePhoneNumberAuthenticationAsync(CompleteLoginWithPhoneNumberModel model)
        {
            //validate the OTP token
            OtpModel otpModel = new()
            {
                PhoneNumber = model.PhoneNumber,
                CountryCode = model.PhoneCode,
                Code = model.OTP,
                Id = model.Token!.Value,
                OtpType = OtpType.Authentication
            };

            ResponseModel<OtpModel> otp = await _otpManager.VerifyAsync(otpModel);
            if (!otp.Status)
            {
                return new ResponseModel<LoginResponseModel>(otp.Message, default, false);
            }

            UserModel? user;

            //if valid, check if the user exists in the database
            user = await _userManager.GetByPhoneNumber(model.PhoneCode!, model.PhoneNumber!);
            //if not, create a new user in the database
            if (user == null)
            {
                AddUserModel addUser = new()
                {
                    PhoneCode = model.PhoneCode!,
                    PhoneNumber = model.PhoneNumber!,
                    Email = string.Empty,
                    AccountType = AccountType.Business,
                    RegistrationType = RegistrationType.PhoneNumber
                };

                ResponseModel<UserModel> result = await _userManager.AddAsync(addUser);
                user = result.Data;
            }

            //generate an auth token and return it
            string generatedToken = GenerateAuthTokenAsync(user!);

            user!.IsFirstLogin = await _businessManager.IsBusinessExist(user!.Id);

            //IsFirstLogin = true; // This should be set based on whether the user has created a business profile or not
            return new ResponseModel<LoginResponseModel>("Authentication Successful.", new LoginResponseModel
            {
                AuthToken = generatedToken,
                User = user,
            }, true);
        }

        public async Task<ResponseModel<LoginResponseModel>> AuthenticateWithSocialAsync(LoginWithSocialModel model)
        {
            //check if the email of the uuser in the database
            UserModel? user;
            user = await _userManager.GetByEmail(model.Email!);

            if (user == null)
            {
                //if not, validate the social token
                GoogleAuthenticationVerificationModel? verificationResult = await _socialAuthProvider.VerifyGoogleTokenAsync(model.Token!);
                if (verificationResult == null)
                {
                    return new ResponseModel<LoginResponseModel>("Invalid Social Authentication Token.", default, false);
                }

                if (!verificationResult.Email!.Equals(model.Email, StringComparison.OrdinalIgnoreCase))
                {
                    return new ResponseModel<LoginResponseModel>("Email Mismatch. Authentication failed", default, false);
                }

                AddUserModel addUser = new()
                {
                    PhoneCode = string.Empty,
                    PhoneNumber = string.Empty,
                    Email = model.Email,
                    AccountType = AccountType.Business,
                    RegistrationType = RegistrationType.Socials
                };

                ResponseModel<UserModel> result = await _userManager.AddAsync(addUser);
                user = result.Data;
            }

            string generatedToken = GenerateAuthTokenAsync(user!);

            user!.IsFirstLogin = !await _businessManager.IsBusinessExist(user!.Id);

            return new ResponseModel<LoginResponseModel>("Authentication Successful.", new LoginResponseModel
            {
                AuthToken = generatedToken,
                User = user,
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
                    new("RegistrationType", user.RegistrationType.ToString()!),
                }),

                Expires = DateTime.UtcNow.AddMinutes(_options.ValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _options.Issuer,
                Audience = _options.Audience
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
