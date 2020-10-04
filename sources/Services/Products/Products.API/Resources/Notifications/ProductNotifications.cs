using Microsoft.Extensions.Localization;

namespace Products.API.Resources.Notifications
{
    public class ProductNotifications : IProductNotifications
    {
        private readonly IStringLocalizer<ProductNotifications> _localizer;

        public ProductNotifications(IStringLocalizer<ProductNotifications> localizer)
        {
            _localizer = localizer;
        }

        public string ProductNameNotBeInvalid => _localizer["ProductNameNotBeInvalid"];
    }
}