using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly ITwilioProvider _twilioProvider;

        public NotificationManager(ITwilioProvider twilioProvider)
        {
            _twilioProvider = twilioProvider ?? throw new ArgumentNullException(nameof(twilioProvider));
        }

        public async Task<BaseResponseModel> SendNotificationAsync(NotificationModel notificationModel)
        {
            if (notificationModel != null)
            {
                bool status = false;

                foreach (DeliveryMode deliveryMode in notificationModel.DeliveryModes)
                {
                    switch (deliveryMode)
                    {
                        case DeliveryMode.Whatsapp:
                            status = await _twilioProvider.SendMessageViaWhatsAppAsync(notificationModel.Recipient!, notificationModel.Content!);
                            break;

                        case DeliveryMode.Email:
                            break;

                        case DeliveryMode.Sms:
                            break;

                        default:
                            break;
                    }
                }

                return new BaseResponseModel(status ? "Notification sent successfully." : "Failed to send notification.", status);
            }

            throw new ArgumentNullException(nameof(notificationModel));
        }
    }
}
