using ProjectR.Backend.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class LoginWithPhoneNumberModel
    {
        [Required(ErrorMessage = "Phone Code is required.")]
        [RegularExpression(@"^\+\d{1,3}$", ErrorMessage = "Invalid phone code format. Use the format +[country code].")]
        public string? PhoneCode { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid phone number format. It should be between 10 to 15 digits.")]
        public string? PhoneNumber { get; set; }
    }
    public class CompleteLoginWithPhoneNumberModel
    {
        [Required(ErrorMessage = "Phone Code is required.")]
        [RegularExpression(@"^\+\d{1,3}$", ErrorMessage = "Invalid phone code format. Use the format +[country code].")]
        public string? PhoneCode { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid phone number format. It should be between 10 to 15 digits.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "OTP is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid OTP format. It should be exactly 6 digits.")]
        public string? OTP { get; set; }

        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public OtpType Type { get; set; }
    }

    public class LoginWithSocialModel
    {
        /// <summary>
        /// This is the token received from the social auth provider (e.g., Google, Facebook)
        /// </summary>
        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        public SocialMediaProvider Type { get; set; }
    }

    public class LoginResponseModel
    {
        public string? AuthToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserModel? User { get; set; }
    }

    public class PhoneNumberLoginResponseModel
    {
        /// <summary>
        /// This is the OTP token for the atual sent to the user's phone number for verification.
        /// It is used to verify the user's ownership of the phone number.
        /// The validation of this token will contain a combination of the phone number and the OTP token.
        /// </summary>
        public string? OtpToken { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        /// <summary>
        /// This is the phone number with the country code that the user has provided during login.
        /// </summary>
        public string? PhoneNumber { get; set; }
        public OtpType? Type { get; set; }
        /// <summary>
        /// For the sake of development, the OTP will be returned till proper whatsapp delivery is sorted
        /// </summary>
        public string? Otp { get; set; }
    }

    public class GoogleAuthenticationVerificationModel
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
