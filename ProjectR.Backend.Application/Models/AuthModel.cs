using ProjectR.Backend.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class LoginWithPhoneNumberModel
    {
        public RegistrationType RegistrationType { get; set; }
        
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
    }

    public class LoginWithSocialModel
    {
        [Required(ErrorMessage = "Registration type is required.")]
        public RegistrationType RegistrationType { get; set; }
        /// <summary>
        /// This is the token received from the social auth provider (e.g., Google, Facebook)
        /// </summary>
        [Required(ErrorMessage = "Token is required.")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
    }

    public class LoginResponseModel
    {
        public string? AuthToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserModel? User { get; set; }
    }
}
