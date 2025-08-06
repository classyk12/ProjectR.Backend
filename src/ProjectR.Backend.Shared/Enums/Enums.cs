using System.ComponentModel;

namespace ProjectR.Backend.Shared.Enums
{
    public enum RecordStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("InActive")]
        InActive,
        [Description("Deleted")]
        Deleted,
        [Description("Archieved")]
        Archieved
    }

    public enum AccountType
    {
        [Description("Business")]
        Business = 1,
        [Description("Customer")]
        Customer
    }

    public enum RegistrationType
    {
        [Description("By Phone")]
        PhoneNumber = 1,
        [Description("By Socials")]
        Socials
    }

    public enum SocialMediaProvider
    {
        [Description("Google")]
        Google = 1,
        [Description("Facebook")]
        Facebook,
        [Description("Apple")]
        Apple
    }

    public enum OtpType
    {
        [Description("Authentication")]
        Authentication = 1,
    }

    public enum DeliveryMode
    {
        [Description("Whatsapp")]
        Whatsapp = 1,
        [Description("Email")]
        Email = 2,
        [Description("Sms")]
        Sms
    }

    public enum BusinessServiceType
    {
        [Description("Onsite")]
        Onsite = 1,
        [Description("Offsite")]
        Offsite = 2,
        [Description("Online")]
        Online
    }

    public enum AppointmentStatus
    {
        [Description("Requested")]
        Requested = 1,
        [Description("Confirmed")]
        Confirmed = 2,
        [Description("Deffered")]
        Deffered,
        [Description("Cancelled")]
        Cancelled
    }
}