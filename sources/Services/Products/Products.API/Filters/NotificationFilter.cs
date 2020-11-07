using System.Net;
using System.Threading.Tasks;
using Base.Domain.Dtos;
using Base.Resources.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Base.API.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var service = (BaseNotificationsContext)context.HttpContext.RequestServices.GetService(typeof(IBaseNotificationsContext));

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