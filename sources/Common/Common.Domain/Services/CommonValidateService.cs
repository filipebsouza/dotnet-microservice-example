using System;
using System.Reflection;
using System.Threading.Tasks;
using Common.Domain.Services.Interfaces;
using Common.Resources.Notifications.Interfaces;
using Microsoft.Extensions.Localization;

namespace Common.Domain.Services
{
    public abstract class CommonValidateService : ICommonValidateService
    {
        private readonly ICommonNotificationContext _notificationContext;
        private readonly IStringLocalizer _stringLocalizer;

        public CommonValidateService(ICommonNotificationContext notificationContext, IStringLocalizer stringLocalizer)
        {
            _notificationContext = notificationContext;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<bool> Validate<Dto>(Dto dto)
        {
            return await Task.Run<bool>(() =>
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

                    if (IsInvalidString(value) || IsInvalidInt(value) || IsInvalidDecimal(value))
                    {
                        _notificationContext.AddNotification(name, _stringLocalizer[name]);
                        return false;
                    }
                }

                return true;
            });
        }

        private bool IsInvalidString(object value)
        {
            return value is string && string.IsNullOrWhiteSpace((string)value);
        }

        private bool IsInvalidInt(object value)
        {
            return value is int && (int)value <= 0;
        }

        private bool IsInvalidDecimal(object value)
        {
            return value is decimal && (decimal)value <= 0;
        }
    }
}