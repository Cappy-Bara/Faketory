using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.API.Dtos
{
    public class TimestampResponse
    {
        public IEnumerable<ConveyorState> Conveyors { get; set; }
        public IEnumerable<PalletDto> Pallets { get; set; }
        public IEnumerable<SensorState> Sensors { get; set; }
    }
}
