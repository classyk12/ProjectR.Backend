using ProjectR.Backend.Shared;
using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    /// <summary>
    /// This represents a customer that has booked a business in the system. We collect this information to be able to retain customer information  and contact them if need be
    /// </summary>
    public class Customer : BaseObject
    {
        public string? PhoneNumber { get; set; }
        public string? PhoneCode { get; set; }
        public string? Email { get; set; }
        /// <summary>
        /// This identifies a user type in the system [Business, Client, Admin etc]
        /// </summary>
        public string? Name { get; set; }
    }
}

