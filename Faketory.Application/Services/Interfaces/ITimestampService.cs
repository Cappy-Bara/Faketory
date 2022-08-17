using System.Threading.Tasks;
using Faketory.Domain.Aggregates;

namespace Faketory.Application.Services.Interfaces
{
    public interface ITimestampService
    {
        public Task DataReading();
        public Task PlcReading();
        public ModifiedUtils SceneHandling();
        public Task DataWriting();
        public Task PlcWriting();
    }
}
