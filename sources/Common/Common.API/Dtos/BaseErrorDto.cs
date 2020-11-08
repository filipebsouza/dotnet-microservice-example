using System.Collections.Generic;
using System.Net;

namespace Common.API.Dtos
{
    public class BaseErrorDto
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public List<string> Errors { get; set; } = new List<string>();
    }
}