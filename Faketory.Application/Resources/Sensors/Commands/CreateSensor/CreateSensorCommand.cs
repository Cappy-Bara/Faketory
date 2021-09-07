using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.CreateSensor
{
    public class CreateSensorCommand : IRequest
    {
        public Guid SlotId { get; set; }
        public string UserEmail { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Byte { get; set; }
        public int Bit { get; set; }
    }
}
