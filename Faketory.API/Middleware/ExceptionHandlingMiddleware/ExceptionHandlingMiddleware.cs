using System;
using System.Net;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using S7.Net;

namespace Faketory.API.Middleware.ExceptionHandlingMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails((HttpStatusCode)ex.StatusCode));
                _logger.LogWarning($"HANDLED EXCEPTION THROWN: CODE - {ex.StatusCode} - {ex.Message}");

            }
            catch (InvalidOperationException ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails(HttpStatusCode.Unauthorized));
                _logger.LogInformation($"UNAUTHORIZED OPERATION: CODE - {HttpStatusCode.Unauthorized} - {ex.Message}");

            }
            catch (PlcException ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails(HttpStatusCode.Unauthorized));
                _logger.LogWarning($"S7.NET EXCEPTION: CODE - {ex.ErrorCode} - {ex.Message}");

            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails());
                _logger.LogError($"!UNHANDLED EXCEPTION THROWN: CODE - 500 - {ex.Message}");
            }
        }

        private static async Task WriteExceptionAsync(HttpContext context, ErrorDetails details)
        {
            var model = new ResponseDetails
            {
                ExceptionMessage = details.ExceptionMessage,
                StatusCode = details.StatusCode
            };
            string error = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.StatusCode = (int)details.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(error);
        }
    }
}
