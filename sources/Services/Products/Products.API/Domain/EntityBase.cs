using System;
using Flunt.Notifications;

namespace Base.Domain
{
    public class EntityBase : Notifiable
    {
        public Guid Id { get; private set; }
    }
}