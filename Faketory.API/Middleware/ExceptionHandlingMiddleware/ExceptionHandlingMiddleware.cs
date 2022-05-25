using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
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
        private readonly TelemetryClient _client;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, TelemetryClient client, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _client = client;
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
                await WriteExceptionAsync(context, SeverityLevel.Critical, ex.ToErrorDetails((HttpStatusCode)ex.StatusCode));
                _logger.LogWarning($"HANDLED EXCEPTION THROWN: CODE - {ex.StatusCode} - {ex.Message}");

            }
            catch (InvalidOperationException ex)
            {
                await WriteExceptionAsync(context, SeverityLevel.Critical, ex.ToErrorDetails(HttpStatusCode.Unauthorized));
                _logger.LogInformation($"UNAUTHORIZED OPERATION: CODE - {HttpStatusCode.Unauthorized} - {ex.Message}");

            }
            catch (PlcException ex)
            {
                await WriteExceptionAsync(context, SeverityLevel.Critical, ex.ToErrorDetails(HttpStatusCode.Unauthorized));
                _logger.LogWarning($"S7.NET EXCEPTION: CODE - {ex.ErrorCode} - {ex.Message}");

            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, SeverityLevel.Critical, ex.ToErrorDetails());
                _logger.LogError($"!UNHANDLED EXCEPTION THROWN: CODE - 500 - {ex.Message}");
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, SeverityLevel level, ErrorDetails details)
        {
            ResponseDetails model = new ResponseDetails();
            model.ExceptionMessage = details.ExceptionMessage;
            model.StatusCode = details.StatusCode;
            string error = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.StatusCode = (int)details.StatusCode;
            context.Response.ContentType = "application/json";

            LogException(_client, level, details, details.Message);

            await context.Response.WriteAsync(error);
        }

        private static void LogException(TelemetryClient telemetryClient, SeverityLevel level, Exception exc, string msg)
        {
            ExceptionTelemetry exceptionTelemetry = new ExceptionTelemetry(exc)
            {
                SeverityLevel = level,
                Message = msg,
            };
            telemetryClient.TrackException(exceptionTelemetry);
        }
    }
}
