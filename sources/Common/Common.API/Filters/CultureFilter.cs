using System.Threading.Tasks;
using Common.Resources.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace Common.API.Filters
{
    public class CultureFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var culture = context.HttpContext.Request.Query["Culture"];

            if (!string.IsNullOrWhiteSpace(culture))
            {
                var service = (JsonStringLocalizer)context.HttpContext.RequestServices.GetService(typeof(IStringLocalizer));
                service.SetCulture(culture);
            }

            await next();
        }
    }
}