using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Common.GlobalExceptionHandling
{

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<RequestDelegate> logger)
        {
            _next = next;
            this.logger = logger;

        }



        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
             
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var message = error.Message;

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                logger.LogError(" ( "+"Service :"+"{"+context.Request.Path+"}" + " - " + message + " ) ");
                var result = JsonSerializer.Serialize(new { isSuccess = false, message = message , status = response.StatusCode });
                await response.WriteAsync(result);
            }
        }
    }


}
