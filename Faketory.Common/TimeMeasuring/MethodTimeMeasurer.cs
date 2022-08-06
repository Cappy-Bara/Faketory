using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Faketory.Common.TimeMeasuring
{
    public class MethodTimeMeasurer<ClassType>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MethodTimeMeasurer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public TOut MeasureTime<TOut>(Func<TOut> function)
        {
            var time = DateTime.Now.ToString("HH:mm:ss:ffff");
            var timeBefore = Stopwatch.GetTimestamp();
            var output = function.Invoke();
            var timeAfter = Stopwatch.GetTimestamp();

            using (var scope = _scopeFactory.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ClassType>>();
                logger.LogInformation($"{time} \t - \t {function.Method.Name} \t - \t EVALUATION TIME: {(timeAfter - timeBefore) / 10000}");
            }

            return output;
        }
        public void MeasureTime(Action function)
        {
            var time = DateTime.Now.ToString("HH:mm:ss:ffff");
            var timeBefore = Stopwatch.GetTimestamp();
            function.Invoke();
            var timeAfter = Stopwatch.GetTimestamp();
            
            using (var scope = _scopeFactory.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILogger<ClassType>>();
                logger.LogInformation($"{time} \t - \t {function.Method.Name} \t - \t EVALUATION TIME: {(timeAfter - timeBefore) / 10000}");
            }
        }
    }
}
