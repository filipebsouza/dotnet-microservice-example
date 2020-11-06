using Common.Resources.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Products.API.Resources.Notifications.Interfaces;
using Products.API.Resources.Notifications;

namespace Products.API.Infra.IoC
{
    public class NotificationsServiceResolver
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, Common.Resources.Notifications.JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer>(service => new JsonStringLocalizer("Resources/Notifications/Product"));
            services.AddLocalization(options => options.ResourcesPath = "Resources/Notifications");
            services.AddScoped(typeof(IProductNotifications), typeof(ProductNotifications));
        }
    }
}