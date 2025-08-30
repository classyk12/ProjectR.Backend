using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Application.Models
{
    public class NotificationModel
    {
        public NotificationModel(DeliveryMode[] deliveryModes, string? recipient, string? content, Dictionary<string, object>? extras)
        {
            DeliveryModes = deliveryModes;
            Recipient = recipient;
            Content = content;
            Extras = extras;
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
        /// <summary>
        /// This holds extra information needed to process a notification
        /// Use Case: When sending via WhatsApp, There exist options to send a plain text or send an interactive message that may contain buttons
        /// Use this to set those values which can then be read by the implementing class
        /// </summary>
        public Dictionary<string, object>? Extras { get; private set; }
    }
}