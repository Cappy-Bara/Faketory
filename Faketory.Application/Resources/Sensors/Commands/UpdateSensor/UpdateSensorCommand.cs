using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorCommand : IRequest
    {
        public string UserEmail { get; set; }
        public Guid SensorId { get; set; }
        public Guid SlotId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Bit { get; set; }
        public int Byte { get; set; }
        public bool NegativeLogic { get; set; }
    }
}
