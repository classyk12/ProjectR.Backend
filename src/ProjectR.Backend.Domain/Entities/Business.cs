using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Domain.Entities
{
    public class Business : BaseObject
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        /// <summary>
        /// Sole Propietorship, Limited Liability etc
        /// </summary>
        public string? Type { get; set; }
        public string? PhoneCode { get; set; }
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Fashion, Lifestyle, Financial etc
        /// </summary>
        public string? Industry { get; set; }
        public string? About { get; set; }
        public BusinessServiceType? ServiceType { get; set; }

        ///To move location information to a different table or nah?
        public string? Location { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        /// <summary>
        /// Business Logo 
        /// </summary>
        public string? Logo { get; set; }
        /// <summary>
        /// This is a unique link that is generated for a business
        /// This can be shared to customers/client to be able to see business schedule and book appointments
        /// </summary>
        public string? ShortLink { get; set; }
    }
}

