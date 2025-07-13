using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Domain.Entities
{
    public class Business : BaseObject
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? PhoneCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Industry { get; set; }
        public string? About { get; set; }
        public BusinessServiceType? ServiceType { get; set; }

        ///To move location information to a different table or nah?
        public string? Location { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}

