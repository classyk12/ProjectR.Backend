
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProjectR.Backend.Application.Models
{
    public class BusinessModel
    {
        public Guid UserId  { get; set; }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? PhoneCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Industry { get; set; }
        public string? About { get; set; }
        public string? Location { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }

    public class AddBusinessModel
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public string? Type { get; set; }
        [Required(ErrorMessage = "Phone Code is required")]
        public string? PhoneCode { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
        public string? Industry { get; set; }
        public string? About { get; set; }
        public string? Location { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
