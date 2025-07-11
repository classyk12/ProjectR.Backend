using System.ComponentModel.DataAnnotations;

namespace ProjectR.Backend.Domain.Entities
{
    public class User : BaseObject
    {
        public string? PhoneNumber { get; set; }
        public string? PhoneCode { get; set; }
        /// <summary>
        /// This identifies a user type in the system [Business, Client, Admin etc]
        /// </summary>
        public AccountType? AccountType { get; set; }
        public RegistrationType? RegistrationType { get; set; }
    }
}

