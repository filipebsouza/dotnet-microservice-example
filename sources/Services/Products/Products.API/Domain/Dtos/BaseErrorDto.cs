using System.Collections.Generic;
using System.Net;

namespace Base.Domain.Dtos
{
    public class BaseErrorDto
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public List<string> Errors { get; set; } = new List<string>();
    }
}