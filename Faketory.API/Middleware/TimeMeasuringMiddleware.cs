using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Middleware
{
    public class TimeMeasuringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeMeasuringMiddleware> _logger;
        public TimeMeasuringMiddleware(RequestDelegate next, ILogger<TimeMeasuringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            var time = DateTime.Now.ToString("HH-mm-ss-ffff");
            watch.Start();

            context.Response.OnStarting(() => {
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                _logger.LogInformation($"{time} \t - \t {context.Request.Method} {context.Request.Path} \t - \t EVALUATION TIME: {responseTimeForCompleteRequest}");
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}
