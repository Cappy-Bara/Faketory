using Faketory.Application.Services.Interfaces;
using Faketory.Common;
using Faketory.Common.TimeMeasuring;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Faketory.Application.Services.Implementations
{
    public class TimeMeasuringTimestampService : ITimestampService, IActivable
    {
        private readonly MethodTimeMeasurer<ITimestampService> methodTimeMeasurer;
        private readonly ITimestampService timestampService;
        public string ConfigurationKey => "MeasureTimestampElementsTimes";

        public TimeMeasuringTimestampService(
            ITimestampService timeStampService,
            IServiceScopeFactory serviceScopeFactory)
        {
            methodTimeMeasurer = new MethodTimeMeasurer<ITimestampService>(serviceScopeFactory);
            timestampService = timeStampService;
        }
        public TimeMeasuringTimestampService(){ }

        public async Task DataReading()
        {
            await methodTimeMeasurer.MeasureTime(async () => await timestampService.DataReading());
        }
        public async Task PlcReading()
        {
            await methodTimeMeasurer.MeasureTime(async () => await timestampService.PlcReading());
        }
        public ModifiedUtils SceneHandling()
        {
            return methodTimeMeasurer.MeasureTime(() => timestampService.SceneHandling());
        }
        public async Task DataWriting()
        {
            await methodTimeMeasurer.MeasureTime(async () => await timestampService.DataWriting());
        }
        public async Task PlcWriting()
        {
            await methodTimeMeasurer.MeasureTime(async () => await timestampService.PlcWriting());
        }
    }
}
