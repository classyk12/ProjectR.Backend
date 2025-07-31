using System.ComponentModel;

namespace ProjectR.Backend.Shared.Enums
{
    public enum RecordStatus
    {
        Active = 1,
        InActive,
        Deleted,
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
        Authentication = 1,
    }

    public enum DeliveryMode
    {
        Whatsapp = 1,
        Email = 2,
        Sms
    }

    public enum BusinessServiceType
    {
        Onsite = 1,
        Offsite = 2,
        Online
    }

    public enum AppointmentStatus
    {
        Requested = 1,
        Confirmed = 2,
        Deffered,
        Cancelled
    }
}