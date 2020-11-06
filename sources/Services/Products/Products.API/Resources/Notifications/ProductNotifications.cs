using Microsoft.Extensions.Localization;
using Products.API.Resources.Notifications.Interfaces;

namespace Products.API.Resources.Notifications
{
    public class ProductNotifications : IProductNotifications
    {
        private readonly IStringLocalizer _localizer;

        public ProductNotifications(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public string ProductNameNotBeInvalid => _localizer["ProductNameNotBeInvalid"];
    }
}