using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Application.Interfaces.Managers
{
    public interface INotificationManager
    {
        /// <summary>
        /// Sends a notification to the user.
        /// </summary>
        /// <param name="notificationModel">The notification model containing the details of the notification.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendNotificationAsync(NotificationModel notificationModel);
    }
}
