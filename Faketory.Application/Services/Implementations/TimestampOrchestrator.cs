using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using System.Threading.Tasks;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampOrchestrator
    {
        private readonly ITimestampService _timestampService;

        public TimestampOrchestrator(ITimestampService timestampService)
        {
            _timestampService = timestampService;
        }

        public async Task<ModifiedUtils> Timestamp()
        {
            await _timestampService.DataReading();

            await _timestampService.PlcReading();

            var modifiedUtils = _timestampService.SceneHandling();

            await _timestampService.DataWriting();

            await _timestampService.PlcWriting();

            return modifiedUtils;
        }
    }
}
