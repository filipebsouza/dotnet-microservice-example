using System.Threading.Tasks;
using Common.Domain.Services;
using Common.Resources.Notifications.Interfaces;
using Microsoft.Extensions.Localization;
using Products.API.Domain.Service.Interfaces;

namespace Products.API.Domain.Service
{
    public class ValidateSaveProductDtoService : CommonValidateService, IValidateSaveProductDtoService
    {
        public ValidateSaveProductDtoService(ICommonNotificationContext notificationContext, IStringLocalizer stringLocalizer) 
            : base(notificationContext, stringLocalizer)
        {
        }
    }
}