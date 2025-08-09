using Newtonsoft.Json;
using ProjectR.Backend.Shared;
using ProjectR.Backend.Shared.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Application.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneCode { get; set; }
        public string? Email { get; set; }
        public AccountType? AccountType { get; set; }
        public RegistrationType? RegistrationType { get; set; }
        /// <summary>
        /// This identifies a user that has onborded but has not yet set up their profile (used by frontend to determine if the user needs to complete their profile)
        /// </summary>
        public bool? IsFirstLogin { get; set; }
    }

    public class AddUserModel
    {
        // [Required(ErrorMessage = "Email is required")]
        // [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        // [Required(ErrorMessage = "Phone number is required")]
        // [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid phone number format. It should be between 10 to 15 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;
        // [Required(ErrorMessage = "Phone Code is required.")]
        // [RegularExpression(@"^\+\d{1,3}$", ErrorMessage = "Invalid phone code format. Use the format +[country code].")]
        public string PhoneCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Account type is required")]
        public AccountType AccountType { get; set; }
        [Required(ErrorMessage = "Registration type is required")]
        public RegistrationType RegistrationType { get; set; }
    }
}
