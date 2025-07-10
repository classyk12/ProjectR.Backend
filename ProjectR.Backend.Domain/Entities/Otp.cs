using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class Otp : BaseObject
    {
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string CountryCode { get; set; }
        [Required]
        public required string? Code { get; set; }
        [Required]
        public required DateTimeOffset ExpiryDate { get; set; }
        [Required]
        public required OtpType OtpType { get; set; }
        public DeliveryMode DeliveryMode { get; set; }
    }
}

