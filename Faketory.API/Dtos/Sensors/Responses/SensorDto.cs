using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Sensors.Responses
{
    public class SensorDto
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsSensing { get; set; }
        public Guid SlotId { get; set; }
        public int Byte { get; set; }
        public int Bit { get; set; }
        public bool NegativeLogic { get; set; }
    }
}
