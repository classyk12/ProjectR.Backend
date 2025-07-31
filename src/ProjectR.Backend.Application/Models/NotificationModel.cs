using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Application.Models
{
    public class NotificationModel
    {
        public NotificationModel(DeliveryMode[] deliveryModes, string? recipient, string? content)
        {
            DeliveryModes = deliveryModes;
            Recipient = recipient;
            Content = content;
        }

        /// <summary>
        /// The delivery modes for the notification, which can include WhatsApp, Email, or SMS.
        /// </summary>
        public DeliveryMode[] DeliveryModes { get; private set; }
        /// <summary>
        /// The recipient of the notification, which could be a phone number for WhatsApp/SMS or an email address for email notifications.
        /// </summary>
        public string? Recipient { get; private set; }
        public string? Content { get; private set; }
    }
}