using System.Collections.Generic;
using Flunt.Notifications;

namespace Base.Resources.Notifications
{
    public interface IBaseNotificationsContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        public bool Invalid { get; }
        public bool Valid { get; }
        public void AddNotification(string property, string message);
    }
}