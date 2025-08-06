using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Shared.Enums;

namespace ProjectR.Backend.Infrastructure.Managers
{
    public class NotificationManager : INotificationManager
    {
        private readonly IWhatsAppProvider _whatsAppProvider;

        public NotificationManager(IWhatsAppProvider whatsAppProvider)
        {
            _whatsAppProvider = whatsAppProvider ?? throw new ArgumentNullException(nameof(whatsAppProvider));
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
                            status = await _whatsAppProvider.SendMessageAsync(notificationModel);
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
