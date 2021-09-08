using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Sensors.Reqests
{
    public class UpdateSensorDto
    {
        public Guid? SensorId { get; set; }
        public Guid? SlotId { get; set; }
        public int? PosX { get; set; }
        public int? PosY { get; set; }
        public int? Bit { get; set; }
        public int? Byte { get; set; }
    }
}
