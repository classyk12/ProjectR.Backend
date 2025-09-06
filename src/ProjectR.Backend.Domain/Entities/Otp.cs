using ProjectR.Backend.Shared;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class Otp : BaseObject
    {
        public string? PhoneNumber { get; set; }
        public string? CountryCode { get; set; }
        public string? Email { get; set; }
        public required string? Code { get; set; }
        [Required]
        public required DateTimeOffset ExpiryDate { get; set; }
        [Required]
        public required OtpType OtpType { get; set; }
        [Required]
        public DeliveryMode DeliveryMode { get; set; }
    }
}

