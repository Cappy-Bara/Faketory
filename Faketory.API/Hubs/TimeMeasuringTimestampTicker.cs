using Faketory.Common;
using Faketory.Common.TimeMeasuring;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Hubs
{
    public class TimeMeasuringTimestampTicker : TimestampTicker, IActivable
    {
        private readonly MethodTimeMeasurer<TimestampTicker> methodTimeMeasurer;
        public string ConfigurationKey => "MeasureSocketTimestamp";

        public TimeMeasuringTimestampTicker(IHubContext<TimestampHub> hub, IServiceScopeFactory scopeFactory) : base(hub, scopeFactory)
        {
            methodTimeMeasurer = new MethodTimeMeasurer<TimestampTicker>(scopeFactory);
        }
        public TimeMeasuringTimestampTicker()
        {
        }

        private protected override void Timestamp(object state)
        {
            var operation = () => base.Timestamp(state);
            methodTimeMeasurer.MeasureTime(operation);
        }
    }
}
