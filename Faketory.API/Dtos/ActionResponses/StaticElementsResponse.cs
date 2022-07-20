using System.Collections.Generic;
using Faketory.API.Dtos.Conveyors.Responses;
using Faketory.API.Dtos.Machine.Responses;
using Faketory.API.Dtos.Sensors.Responses;

namespace Faketory.API.Dtos.ActionResponses
{
    public class StaticElementsResponse
    {
        public List<ConveyorDto> Conveyors { get; set; }
        public List<SensorDto> Sensors { get; set; }
        public List<MachineDto> Machines { get; set; }
    }
}
