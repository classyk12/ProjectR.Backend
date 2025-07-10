using System.ComponentModel;

namespace ProjectR.Backend.Domain
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
        Business = 1,
        Customer
    }

    public enum RegistrationType
    {
        [Description("By Phone")]
        PhoneNumber = 1,
        [Description("By Socials")]
        Socials
    }

    public enum OtpType
    {
        Onboarding = 1,
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
}