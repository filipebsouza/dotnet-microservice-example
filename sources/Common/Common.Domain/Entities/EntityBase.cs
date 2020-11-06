using System;
using Flunt.Notifications;

namespace Common.Domain.Entities
{
    public class EntityBase : Notifiable
    {
        public Guid Id { get; }
    }  
}