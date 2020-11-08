using System.Net;
using System.Threading.Tasks;
using Common.API.Dtos;
using Common.Resources.Notifications;
using Common.Resources.Notifications.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Common.API.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var service = (CommonNotificationContext)context.HttpContext.RequestServices.GetService(typeof(ICommonNotificationContext));

            if (service != null && service.Invalid)
            {
                var json = new BaseErrorDto();
                foreach (var error in service.Notifications)
                {
                    json.Errors.Add(error.Message);
                }

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(json));
            }
            else
            {
                await next();
            }
        }
    }
}