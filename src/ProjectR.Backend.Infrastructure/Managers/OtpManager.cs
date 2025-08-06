using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Shared.Enums;
using ProjectR.Backend.Shared.Helpers;
using ProjectR.Backend.Application.Settings;
using Microsoft.Extensions.Options;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class OtpManager : IOtpManager
    {
        private readonly IOtpRepository _otpRepository;
        private readonly OtpSettings _settings;

        public OtpManager(IOtpRepository otpRepository, IOptions<OtpSettings> settings)
        {
            _otpRepository = otpRepository ?? throw new ArgumentNullException(nameof(otpRepository));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<ResponseModel<OtpModel>> AddAsync(OtpModel model)
        {
            if (model.DeliveryMode.Equals(DeliveryMode.Email) && (model.Email == null || !model.Email.IsValidEmail()))
            {
                return new ResponseModel<OtpModel>(
                message: "Email is invalid. Provide a valid Email and try again",
                data: default,
                status: false);
            }

            if (!CanSendOtpToPhoneNumber(model))
            {
                return new ResponseModel<OtpModel>(
                message: "PhoneNumber is invalid. Provide a valid Phone Number and Phone Code and try again",
                data: default,
                status: false);
            }

            (string code, DateTimeOffset expiry) = GenerateCode();
            model.ExpiryDate = expiry;
            model.Code = code;

            OtpModel result = await _otpRepository.AddAsync(model);
            return new ResponseModel<OtpModel>(message: "Successful", data: result);
        }

        public async Task<ResponseModel<OtpModel>> VerifyAsync(OtpModel model)
        {
            if (_settings.UseMock)
            {
                return new ResponseModel<OtpModel>(
                message: "OTP Verified Successfully.",
                data: model,
                status: true);
            }

            model.Code = model.Code?.Base64Encode();
            OtpModel? otp = await _otpRepository.GetAsync(model);

            if (otp == null)
            {
                return new ResponseModel<OtpModel>(
                 message: "Invalid OTP",
                 data: default,
                 status: false);
            }

            if (otp.RecordStatus != RecordStatus.Active)
            {
                return new ResponseModel<OtpModel>(
                message: "Invalid OTP",
                data: default,
                status: false);
            }

            bool isExpired = DateTimeOffset.Now > otp.ExpiryDate;

            otp.RecordStatus = RecordStatus.InActive;
            await _otpRepository.UpdateAsync(model);

            return new ResponseModel<OtpModel>(
            message: isExpired ? "OTP is Expired." : "OTP Verified Successfully.",
            data: isExpired ? default : otp,
            status: !isExpired);
        }

        private static bool CanSendOtpToPhoneNumber(OtpModel model)
        {
            bool isDigit = model.PhoneNumber != null && model.PhoneNumber.IsDigitOnly() && model.PhoneNumber.IsValidPhone();
            bool isValidCountryCode = model.CountryCode != null && model.CountryCode.IsValidPhoneCode();
            return (model.DeliveryMode.Equals(DeliveryMode.Whatsapp) || model.DeliveryMode.Equals(DeliveryMode.Sms)) && isDigit && isValidCountryCode;
        }

        private (string code, DateTimeOffset expiry) GenerateCode()
        {
            Random rnd = new();
            string random = rnd.Next(1, 99999).ToString().PadLeft(5, '0');
            DateTimeOffset expiry = DateTimeOffset.Now.AddMinutes(_settings.ExpiryInMin);
            return (random, expiry);
        }
    }
}
