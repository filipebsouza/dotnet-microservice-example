using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Domain.Services.Interfaces;
using Common.Resources.Notifications.Interfaces;

namespace Common.Domain.Services
{
    public abstract class CommonValidateService : ICommonValidateService
    {
        private readonly ICommonNotificationContext _notificationContext;

        public CommonValidateService(ICommonNotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task<bool> Validate<Dto>(Dto dto)
        {
            var properties = (typeof(Dto)).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(dto, null);

                if (property.PropertyType.IsGenericType)
                {
                    var isNullableType = property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                    if (isNullableType) continue;
                }                

                if (value == null)
                {
                    _notificationContext.AddNotification(name, )
                }

                if (value is int)
                {

                }

                //Nullable.GetUnderlyingType(property.DeclaringType)

            }

            return true;
        }
    }
}