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
    public class TimeMeasuringTimestampService : TimestampService, IActivable
    {
        public TimeMeasuringTimestampService()
        {

        }

        public TimeMeasuringTimestampService(
            IMediator mediator,
            IPalletRepository palletRepo,
            ISensorRepository sensorRepo,
            IConveyorRepository conveyorRepo,
            IMachineRepository machinesRepo,
            IServiceScopeFactory serviceScopeFactory) : base(mediator, palletRepo, sensorRepo, conveyorRepo, machinesRepo)
        {
            methodTimeMeasurer = new MethodTimeMeasurer<ITimestampService>(serviceScopeFactory);
        }

        private readonly MethodTimeMeasurer<ITimestampService> methodTimeMeasurer;
        
        public string ConfigurationKey => "MeasureTimestampElementsTimes";

        protected override async Task DatabaseReading(string userEmail)
        {
            await methodTimeMeasurer.MeasureTime(async () => await base.DatabaseReading(userEmail));
        }
        protected override async Task PlcReading()
        {
            await methodTimeMeasurer.MeasureTime(async () => await base.PlcReading());
        }
        protected override ModifiedUtils SceneHandling()
        {
            return methodTimeMeasurer.MeasureTime(() => base.SceneHandling());
        }
        protected override async Task DatabaseWriting()
        {
            await methodTimeMeasurer.MeasureTime(async () => await base.DatabaseWriting());
        }
        protected override async Task PlcWriting()
        {
            await methodTimeMeasurer.MeasureTime(async () => await base.PlcWriting());
        }
    }
}
