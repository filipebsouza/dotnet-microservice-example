using System.Collections.Generic;
using Flunt.Notifications;

namespace Common.Resources.Notifications.Interfaces
{
    public interface ICommonNotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Invalid { get; }
        bool Valid { get; }
        void AddNotification(string property, string message);
    }
}