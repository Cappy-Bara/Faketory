using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Faketory.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (DomainException e)
            {
                context.Response.StatusCode = e.StatusCode;
                await context.Response.WriteAsync(e.Message);
                _logger.LogError($"HANDLED EXCEPTION THROWN: CODE - {e.StatusCode} - {e.Message}");
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);
                _logger.LogError($"!UNHANDLED EXCEPTION THROWN: CODE - 500 - {e.Message}");

            }

        }
    }
}
