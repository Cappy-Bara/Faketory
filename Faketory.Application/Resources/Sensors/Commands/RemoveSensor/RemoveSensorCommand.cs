using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.RemoveSensor
{
    public class RemoveSensorCommand : IRequest
    {
        public Guid SensorId { get; set; }
        public string UserEmail { get; set; }
    }
}
